using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SocialNetwork.Core;

namespace SocialNetwork.App
{
    public partial class Form1 : Form
    {
        private readonly NetworkManager _network = new();
        private System.Collections.Generic.List<Recommendation> _currentRecommendations = new();
        private int _selectedCandidateId = -1;
        private System.Collections.Generic.Dictionary<int, Rectangle> _nodePositions = new();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _network.GenerateRandomNetwork(1000);

            comboBoxUsers.DisplayMember = "Value";
            comboBoxUsers.ValueMember = "Key";
            comboBoxUsers.DataSource = _network.GetAllUsers()
                .Select(kvp => new { Key = kvp.Key, Value = kvp.Value.Name })
                .ToList();

            if (comboBoxUsers.Items.Count > 0)
                comboBoxUsers.SelectedIndex = 0;

            UpdateGlobalStats();
            lblStatus.Text = "✓ Готов к работе";
        }

        private void UpdateGlobalStats()
        {
            var stats = _network.CalculateGlobalStatistics(10);
            lblAvgRecs.Text = $"Ср. рекомендаций: {stats.AverageRecommendationsPerUser:F1}";
            lblPercentRecs.Text = $"Охват: {stats.PercentageWithRecommendations:F1}%";
            progressBarRecs.Value = Math.Min(100, (int)stats.PercentageWithRecommendations);
        }

        private void ComboBoxUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxUsers.SelectedValue == null) return;

            int userId = (int)comboBoxUsers.SelectedValue;
            var user = _network.GetUser(userId);

            lblUserName.Text = user?.Name ?? "—";
            lblUserFriends.Text = $"Друзей: {user?.Friends.Count ?? 0}";

            GenerateRecommendations();
        }

        private void GenerateRecommendations()
        {
            if (comboBoxUsers.SelectedValue == null) return;

            lblLoading.Visible = true;
            pictureBoxGraph.Invalidate();

            int userId = (int)comboBoxUsers.SelectedValue;
            _currentRecommendations = _network.GetTopRecommendations(userId, 10);

            // Показываем все рекомендации без фильтрации
            var displayData = _currentRecommendations
                .Select((rec, index) => new
                {
                    Rank = (index + 1).ToString(),
                    Candidate = rec.CandidateName,
                    MutualFriends = rec.MutualFriendsCount.ToString(),
                    Coefficient = rec.ConnectivityCoefficient
                })
                .ToList();

            dataGridViewRecs.DataSource = null;
            dataGridViewRecs.DataSource = displayData;

            // Настройка заголовков колонок
            if (dataGridViewRecs.Columns["Rank"] != null)
                dataGridViewRecs.Columns["Rank"].HeaderText = "№";
            if (dataGridViewRecs.Columns["Candidate"] != null)
                dataGridViewRecs.Columns["Candidate"].HeaderText = "Кандидат";
            if (dataGridViewRecs.Columns["MutualFriends"] != null)
            {
                dataGridViewRecs.Columns["MutualFriends"].HeaderText = "Общие";
                dataGridViewRecs.Columns["MutualFriends"].Width = 80;
            }
            if (dataGridViewRecs.Columns["Coefficient"] != null)
            {
                dataGridViewRecs.Columns["Coefficient"].HeaderText = "Связность";
                dataGridViewRecs.Columns["Coefficient"].Width = 90;
                dataGridViewRecs.Columns["Coefficient"].DefaultCellStyle.Format = "P1";
            }

            lblResults.Text = $"Найдено {_currentRecommendations.Count} кандидатов";

            lblLoading.Visible = false;
            lblGraphInfo.Text = $"Алгоритм: BFS (глубина 2) | Вершин: {_currentRecommendations.Count + _network.GetUser(userId)?.Friends.Count ?? 0}";
            lblStatus.Text = "✓ Анализ завершён";
        }

        private void DataGridViewRecs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Игнорируем клики по заголовкам
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            try
            {
                var row = dataGridViewRecs.Rows[e.RowIndex];

                // Проверяем, что есть данные
                if (row.DataBoundItem == null)
                    return;

                // Получаем имя кандидата из колонки "Candidate"
                var candidateNameCell = row.Cells["Candidate"];
                if (candidateNameCell.Value == null)
                    return;

                string candidateName = candidateNameCell.Value.ToString();

                // Ищем кандидата в списке рекомендаций
                var candidate = _currentRecommendations.FirstOrDefault(r => r.CandidateName == candidateName);

                if (candidate != null)
                {
                    _selectedCandidateId = candidate.CandidateId;
                    ShowCommonFriends(candidate);
                    pictureBoxGraph.Invalidate();
                }
                else
                {
                    lblStatus.Text = "⚠ Кандидат не найден";
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "⚠ Ошибка при выборе кандидата";
                // В отладке можно раскомментировать:
                // MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowCommonFriends(Recommendation candidate)
        {
            try
            {
                flowLayoutPanelCommonFriends.Controls.Clear();
                lblNoSelection.Visible = false;

                var firstName = candidate.CandidateName.Split(' ')[0];
                lblDetailsTitle.Text = $"👥 Общие с {firstName}";

                var userId = (int)comboBoxUsers.SelectedValue;
                var targetUser = _network.GetUser(userId);
                var candidateUser = _network.GetUser(candidate.CandidateId);

                if (targetUser == null || candidateUser == null)
                {
                    lblNoSelection.Visible = true;
                    lblNoSelection.Text = "Не удалось загрузить данные";
                    return;
                }

                var commonFriends = targetUser.Friends
                    .Intersect(candidateUser.Friends)
                    .ToList();

                if (commonFriends.Count == 0)
                {
                    lblNoSelection.Visible = true;
                    lblNoSelection.Text = "Нет общих друзей";
                    return;
                }

                foreach (var friendId in commonFriends)
                {
                    var friend = _network.GetUser(friendId);
                    if (friend == null) continue;

                    var lbl = new Label
                    {
                        Text = $"• {friend.Name}",
                        Font = new Font("Segoe UI", 9F),
                        ForeColor = Color.FromArgb(200, 200, 200),
                        AutoSize = true,
                        Margin = new Padding(5, 3, 5, 3),
                        Cursor = Cursors.Hand,
                        Tag = friendId // Сохраняем ID друга
                    };

                    flowLayoutPanelCommonFriends.Controls.Add(lbl);
                }

                lblStatus.Text = $"✓ Найдено {commonFriends.Count} общих друзей";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "⚠ Ошибка при загрузке общих друзей";
                flowLayoutPanelCommonFriends.Controls.Clear();
                lblNoSelection.Visible = true;
                lblNoSelection.Text = "Произошла ошибка";
            }
        }

        private void PictureBoxGraph_Paint(object sender, PaintEventArgs e)
        {
            if (_currentRecommendations.Count == 0 || comboBoxUsers.SelectedValue == null)
            {
                e.Graphics.DrawString("Выберите пользователя для анализа",
                    new Font("Segoe UI", 14, FontStyle.Italic), Brushes.Gray,
                    pictureBoxGraph.Width / 2 - 150, pictureBoxGraph.Height / 2);
                return;
            }

            int userId = (int)comboBoxUsers.SelectedValue;
            var targetUser = _network.GetUser(userId);
            if (targetUser == null) return;

            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int cx = pictureBoxGraph.Width / 2;
            int cy = pictureBoxGraph.Height / 2;
            int radiusFriends = 100;
            int radiusRecs = 200;

            _nodePositions.Clear();

            // Рисуем связи
            var friends = targetUser.Friends.Take(12).ToList();
            foreach (var fId in friends)
            {
                int index = friends.IndexOf(fId);
                double angle = 2 * Math.PI * index / friends.Count;
                int fx = cx + (int)(radiusFriends * Math.Cos(angle));
                int fy = cy + (int)(radiusFriends * Math.Sin(angle));
                g.DrawLine(new Pen(Color.FromArgb(80, 0, 150, 255), 2), cx, cy, fx, fy);
            }

            for (int i = 0; i < _currentRecommendations.Count; i++)
            {
                double angle = 2 * Math.PI * i / _currentRecommendations.Count;
                int rx = cx + (int)(radiusRecs * Math.Cos(angle));
                int ry = cy + (int)(radiusRecs * Math.Sin(angle));

                var intensity = Math.Min(255, 100 + _currentRecommendations[i].MutualFriendsCount * 30);
                g.DrawLine(new Pen(Color.FromArgb(150, 0, intensity, 100), 2), cx, cy, rx, ry);
            }

            // Центральный узел
            DrawNode(g, cx, cy, 35, Color.FromArgb(180, 100, 255), targetUser.Name.Split(' ')[0], true);
            _nodePositions[userId] = new Rectangle(cx - 35, cy - 35, 70, 70);

            // Друзья
            for (int i = 0; i < friends.Count; i++)
            {
                double angle = 2 * Math.PI * i / friends.Count;
                int fx = cx + (int)(radiusFriends * Math.Cos(angle));
                int fy = cy + (int)(radiusFriends * Math.Sin(angle));
                var friend = _network.GetUser(friends[i]);
                DrawNode(g, fx, fy, 20, Color.FromArgb(0, 150, 255), friend?.Name.Split(' ')[0] ?? "Д", false);
                _nodePositions[friends[i]] = new Rectangle(fx - 20, fy - 20, 40, 40);
            }

            // Рекомендации
            for (int i = 0; i < _currentRecommendations.Count; i++)
            {
                double angle = 2 * Math.PI * i / _currentRecommendations.Count;
                int rx = cx + (int)(radiusRecs * Math.Cos(angle));
                int ry = cy + (int)(radiusRecs * Math.Sin(angle));

                var intensity = Math.Min(255, 100 + _currentRecommendations[i].MutualFriendsCount * 30);
                var color = Color.FromArgb(0, intensity, 100 + intensity / 3);

                DrawNode(g, rx, ry, 22, color, _currentRecommendations[i].CandidateName.Split(' ')[0], false);
                _nodePositions[_currentRecommendations[i].CandidateId] = new Rectangle(rx - 22, ry - 22, 44, 44);
            }
        }

        private void DrawNode(Graphics g, int x, int y, int size, Color color, string label, bool isCenter)
        {
            g.FillEllipse(new SolidBrush(color), x - size, y - size, size * 2, size * 2);
            g.DrawEllipse(new Pen(Color.White, isCenter ? 3 : 1), x - size, y - size, size * 2, size * 2);

            var font = new Font("Segoe UI", isCenter ? 10 : 7, FontStyle.Bold);
            var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            g.DrawString(label, font, Brushes.White, new RectangleF(x - size, y - size, size * 2, size * 2), format);
        }

        private void PictureBoxGraph_MouseMove(object sender, MouseEventArgs e)
        {
            foreach (var kvp in _nodePositions)
            {
                if (kvp.Value.Contains(e.Location))
                {
                    var user = _network.GetUser(kvp.Key);
                    var rec = _currentRecommendations.FirstOrDefault(r => r.CandidateId == kvp.Key);

                    string tooltip = user?.Name ?? "Unknown";
                    if (rec != null)
                        tooltip += $"\nОбщих друзей: {rec.MutualFriendsCount}\nСвязность: {rec.ConnectivityCoefficient:P1}";

                    toolTip1.SetToolTip(pictureBoxGraph, tooltip);
                    return;
                }
            }
            toolTip1.SetToolTip(pictureBoxGraph, "");
        }

        private ToolTip toolTip1 = new ToolTip();
    }
}   