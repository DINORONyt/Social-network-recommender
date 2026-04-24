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

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Инициализация сети при запуске
            _network.GenerateRandomNetwork(1000);

            // Заполнение ComboBox с русскими именами
            comboBoxUsers.DisplayMember = "Value";
            comboBoxUsers.ValueMember = "Key";
            comboBoxUsers.DataSource = _network.GetAllUsers()
                .Select(kvp => new { Key = kvp.Key, Value = kvp.Value.Name })
                .ToList();

            // Выбор первого пользователя по умолчанию
            if (comboBoxUsers.Items.Count > 0)
                comboBoxUsers.SelectedIndex = 0;
        }

        private void btnGenerateRecs_Click(object sender, EventArgs e)
        {
            if (comboBoxUsers.SelectedValue == null) return;

            int selectedUserId = (int)comboBoxUsers.SelectedValue;

            try
            {
                _currentRecommendations = _network.GetTopRecommendations(selectedUserId, 10);

                // Привязка к DataGridView с нумерацией
                var displayData = _currentRecommendations
                    .Select((rec, index) => new
                    {
                        Rank = index + 1,
                        Candidate = rec.CandidateName,
                        MutualFriends = rec.MutualFriendsCount,
                        Coefficient = rec.ConnectivityCoefficient,
                        TotalFriends = rec.FriendsCount
                    })
                    .ToList();

                dataGridViewRecs.DataSource = null;
                dataGridViewRecs.DataSource = displayData;

                // Перерисовка визуализации
                pictureBoxNetwork.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnStats_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            var stats = _network.CalculateGlobalStatistics(10);
            Cursor = Cursors.Default;

            lblStats.Text = $"📊 ОБЩАЯ СТАТИСТИКА СОЦИАЛЬНОЙ СЕТИ\n\n" +
                            $"👥 Всего пользователей: {stats.TotalUsers}\n\n" +
                            $"✅ Пользователей с рекомендациями: {stats.UsersWithRecommendations}\n\n" +
                            $"📈 Процент пользователей с рекомендациями: {stats.PercentageWithRecommendations:F2}%\n\n" +
                            $"📊 Среднее количество рекомендаций на пользователя: {stats.AverageRecommendationsPerUser:F2}";
        }

        private void pictureBoxNetwork_Paint(object sender, PaintEventArgs e)
        {
            if (_currentRecommendations == null || _currentRecommendations.Count == 0 || comboBoxUsers.SelectedValue == null)
            {
                e.Graphics.DrawString("Выберите пользователя и нажмите «Найти друзей»",
                    new Font("Segoe UI", 12), Brushes.Gray, 120, 160);
                return;
            }

            int userId = (int)comboBoxUsers.SelectedValue;
            var targetUser = _network.GetUser(userId);
            if (targetUser == null) return;

            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int cx = pictureBoxNetwork.Width / 2;
            int cy = pictureBoxNetwork.Height / 2;
            int radiusFriends = 70;
            int radiusRecs = 150;

            // Рисуем связи с друзьями
            var friends = targetUser.Friends.ToList();
            for (int i = 0; i < friends.Count; i++)
            {
                double angle = 2 * Math.PI * i / Math.Min(friends.Count, 12);
                int fx = cx + (int)(radiusFriends * Math.Cos(angle));
                int fy = cy + (int)(radiusFriends * Math.Sin(angle));
                g.DrawLine(new Pen(Color.FromArgb(100, 150, 200), 1.5f), cx, cy, fx, fy);
            }

            // Рисуем связи с рекомендованными кандидатами
            foreach (var rec in _currentRecommendations)
            {
                double angle = 2 * Math.PI * rec.MutualFriendsCount / 10;
                int rx = cx + (int)(radiusRecs * Math.Cos(angle));
                int ry = cy + (int)(radiusRecs * Math.Sin(angle));

                g.DrawLine(new Pen(Color.FromArgb(200, 150, 50), 2)
                {
                    DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
                }, cx, cy, rx, ry);
            }

            // Рисуем центрального пользователя
            DrawNode(g, cx, cy, targetUser.Name.Split(' ')[0],
                Color.FromArgb(0, 102, 204), Color.White, 25, true);

            // Рисуем друзей (до 12)
            for (int i = 0; i < Math.Min(friends.Count, 12); i++)
            {
                double angle = 2 * Math.PI * i / Math.Min(friends.Count, 12);
                int fx = cx + (int)(radiusFriends * Math.Cos(angle));
                int fy = cy + (int)(radiusFriends * Math.Sin(angle));
                DrawNode(g, fx, fy, $"Друг {i + 1}",
                    Color.FromArgb(100, 180, 220), Color.White, 10, false);
            }

            // Рисуем рекомендации
            for (int i = 0; i < _currentRecommendations.Count; i++)
            {
                double angle = 2 * Math.PI * i / _currentRecommendations.Count;
                int rx = cx + (int)(radiusRecs * Math.Cos(angle));
                int ry = cy + (int)(radiusRecs * Math.Sin(angle));
                var recName = _currentRecommendations[i].CandidateName.Split(' ');
                string displayName = recName.Length > 0 ? recName[0] : "User";
                DrawNode(g, rx, ry, displayName,
                    Color.FromArgb(255, 180, 100), Color.White, 12, false);
            }
        }

        private void DrawNode(Graphics g, int x, int y, string label,
            Color fillColor, Color textColor, int size, bool isCenter)
        {
            // Рисуем круг
            g.FillEllipse(new SolidBrush(fillColor), x - size, y - size, size * 2, size * 2);
            g.DrawEllipse(new Pen(Color.FromArgb(100, 100, 100), 2), x - size, y - size, size * 2, size * 2);

            // Рисуем текст
            var font = new Font("Segoe UI", isCenter ? 10 : 7, FontStyle.Bold);
            var textFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            g.DrawString(label, font, new SolidBrush(textColor),
                new RectangleF(x - size, y - size - 15, size * 2, size * 2), textFormat);
        }
    }
}