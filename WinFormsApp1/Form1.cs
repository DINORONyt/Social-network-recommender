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
            _network.GenerateRandomNetwork(1000);

            comboBoxUsers.DisplayMember = "Value";
            comboBoxUsers.ValueMember = "Key";
            comboBoxUsers.DataSource = _network.GetAllUsers()
                .Select(kvp => new { Key = kvp.Key, Value = kvp.Value.Name })
                .ToList();

            if (comboBoxUsers.Items.Count > 0)
                comboBoxUsers.SelectedIndex = 0;
        }

        private void BtnGenerateRecs_Click(object sender, EventArgs e)
        {
            if (comboBoxUsers.SelectedValue == null) return;

            int selectedUserId = (int)comboBoxUsers.SelectedValue;

            try
            {
                _currentRecommendations = _network.GetTopRecommendations(selectedUserId, 10);

                var displayData = _currentRecommendations
                    .Select((rec, index) => new
                    {
                        Номер = (index + 1).ToString(),
                        Друг = rec.CandidateName,
                        ОбщихДрузей = rec.MutualFriendsCount.ToString(),
                        Связность = rec.ConnectivityCoefficient,
                        ВсегоДрузей = rec.FriendsCount.ToString()
                    })
                    .ToList();

                dataGridViewRecs.DataSource = null;
                dataGridViewRecs.DataSource = displayData;

                groupBoxRecs.Text = $"Топ-10 рекомендаций для: {_network.GetUser(selectedUserId)?.Name}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnStats_Click(object sender, EventArgs e)
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
    }
}