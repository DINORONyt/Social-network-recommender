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
        private List<Recommendation> _currentRecommendations = new();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Инициализация сети при запуске
            _network.GenerateRandomNetwork(1000);

            // Заполнение ComboBox
            comboBoxUsers.DisplayMember = "Value.Name";
            comboBoxUsers.ValueMember = "Value.Id";
            comboBoxUsers.DataSource = new System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<int, User>>(_network.GetAllUsers().ToList());

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

                // Привязка к DataGridView
                dataGridViewRecs.DataSource = null;
                dataGridViewRecs.DataSource = _currentRecommendations;

                // Перерисовка визуализации
                pictureBoxNetwork.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnStats_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            var stats = _network.CalculateGlobalStatistics(10);
            Cursor = Cursors.Default;

            lblStats.Text = $"Всего пользователей: {stats.TotalUsers}\n" +
                            $"Пользователей с рекомендациями: {stats.UsersWithRecommendations}\n" +
                            $"Процент пользователей с рекомендациями: {stats.PercentageWithRecommendations:F2}%\n" +
                            $"Среднее кол-во рекомендаций на пользователя: {stats.AverageRecommendationsPerUser:F2}";
        }

        private void pictureBoxNetwork_Paint(object sender, PaintEventArgs e)
        {
            if (_currentRecommendations == null || _currentRecommendations.Count == 0 || comboBoxUsers.SelectedValue == null)
            {
                e.Graphics.DrawString("Выберите пользователя и нажмите 'Найти друзей'",
                    Font, Brushes.Gray, 10, 10);
                return;
            }

            int userId = (int)comboBoxUsers.SelectedValue;
            var targetUser = _network.GetUser(userId);
            if (targetUser == null) return;

            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int cx = pictureBoxNetwork.Width / 2;
            int cy = pictureBoxNetwork.Height / 2;
            int radiusFriends = 80;
            int radiusRecs = 180;

            // 1. Рисуем связи текущего пользователя с друзьями
            g.DrawLine(Pens.Black, cx, cy, cx, cy); // Центр
            var friends = targetUser.Friends.ToList();
            for (int i = 0; i < friends.Count; i++)
            {
                double angle = 2 * Math.PI * i / friends.Count;
                int fx = cx + (int)(radiusFriends * Math.Cos(angle));
                int fy = cy + (int)(radiusFriends * Math.Sin(angle));
                g.DrawLine(Pens.LightBlue, cx, cy, fx, fy);
            }

            // 2. Рисуем связи с рекомендованными кандидатами (пунктиром)
            foreach (var rec in _currentRecommendations)
            {
                double angle = 2 * Math.PI * rec.MutualFriendsCount / 10; // Упрощённое позиционирование
                int rx = cx + (int)(radiusRecs * Math.Cos(angle));
                int ry = cy + (int)(radiusRecs * Math.Sin(angle));

                // Рисуем линию к ближайшему общему другу (визуально)
                g.DrawLine(new Pen(Color.Orange, 2) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash }, cx, cy, rx, ry);
            }

            // 3. Рисуем узлы
            DrawNode(g, cx, cy, targetUser.Name, Color.Red, 20);

            for (int i = 0; i < Math.Min(friends.Count, 12); i++) // Рисуем часть друзей, чтобы не перегружать
            {
                double angle = 2 * Math.PI * i / friends.Count;
                int fx = cx + (int)(radiusFriends * Math.Cos(angle));
                int fy = cy + (int)(radiusFriends * Math.Sin(angle));
                DrawNode(g, fx, fy, $"F{i + 1}", Color.LightBlue, 8);
            }

            for (int i = 0; i < _currentRecommendations.Count; i++)
            {
                double angle = 2 * Math.PI * i / _currentRecommendations.Count;
                int rx = cx + (int)(radiusRecs * Math.Cos(angle));
                int ry = cy + (int)(radiusRecs * Math.Sin(angle));
                DrawNode(g, rx, ry, _currentRecommendations[i].CandidateName, Color.Orange, 10);
            }
        }

        private void DrawNode(Graphics g, int x, int y, string label, Color color, int size)
        {
            g.FillEllipse(new SolidBrush(color), x - size, y - size, size * 2, size * 2);
            g.DrawEllipse(Pens.Black, x - size, y - size, size * 2, size * 2);
            g.DrawString(label, new Font("Arial", 7), Brushes.Black, x - 20, y - size - 12);
        }
    }
}