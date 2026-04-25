namespace SocialNetwork.App
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            // ===== ЦВЕТОВАЯ СХЕМА (Тёмная тема) =====
            Color bgColor = Color.FromArgb(30, 30, 30);
            Color panelColor = Color.FromArgb(45, 45, 45);
            Color accentBlue = Color.FromArgb(0, 150, 255);
            Color accentGreen = Color.FromArgb(0, 255, 150);
            Color accentPurple = Color.FromArgb(180, 100, 255);
            Color textColor = Color.FromArgb(240, 240, 240);
            Color mutedColor = Color.FromArgb(120, 120, 120);

            // ===== ВЕРХНЯЯ ПАНЕЛЬ =====
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.comboBoxUsers = new System.Windows.Forms.ComboBox();
            this.lblUserLabel = new System.Windows.Forms.Label();
            this.panelUserCard = new System.Windows.Forms.Panel();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblUserFriends = new System.Windows.Forms.Label();
            this.panelGlobalStats = new System.Windows.Forms.Panel();
            this.lblAvgRecs = new System.Windows.Forms.Label();
            this.lblPercentRecs = new System.Windows.Forms.Label();
            this.progressBarRecs = new System.Windows.Forms.ProgressBar();

            // ===== ЛЕВАЯ ПАНЕЛЬ (Топ-10) =====
            this.panelLeft = new System.Windows.Forms.Panel();
            this.lblRecsTitle = new System.Windows.Forms.Label();
            this.dataGridViewRecs = new System.Windows.Forms.DataGridView();
            this.colRank = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCandidate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMutual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCoeff = new System.Windows.Forms.DataGridViewTextBoxColumn();

            // ===== ЦЕНТРАЛЬНАЯ ОБЛАСТЬ (Граф) =====
            this.panelCenter = new System.Windows.Forms.Panel();
            this.pictureBoxGraph = new System.Windows.Forms.PictureBox();
            this.lblGraphInfo = new System.Windows.Forms.Label();
            this.lblLoading = new System.Windows.Forms.Label();

            // ===== ПРАВАЯ ПАНЕЛЬ (Детали) =====
            this.panelRight = new System.Windows.Forms.Panel();
            this.lblDetailsTitle = new System.Windows.Forms.Label();
            this.flowLayoutPanelCommonFriends = new System.Windows.Forms.FlowLayoutPanel();
            this.lblNoSelection = new System.Windows.Forms.Label();

            // ===== НИЖНЯЯ ПАНЕЛЬ (Статус) =====
            this.panelBottom = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblResults = new System.Windows.Forms.Label();

            // ===== ИНИЦИАЛИЗАЦИЯ =====
            this.panelTop.SuspendLayout();
            this.panelUserCard.SuspendLayout();
            this.panelGlobalStats.SuspendLayout();
            this.panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRecs)).BeginInit();
            this.panelCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGraph)).BeginInit();
            this.panelRight.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();

            // ===== ВЕРХНЯЯ ПАНЕЛЬ =====
            this.panelTop.BackColor = panelColor;
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Height = 100;
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Controls.Add(this.lblUserLabel);
            this.panelTop.Controls.Add(this.comboBoxUsers);
            this.panelTop.Controls.Add(this.panelUserCard);
            this.panelTop.Controls.Add(this.panelGlobalStats);

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = accentBlue;
            this.lblTitle.Location = new System.Drawing.Point(15, 10);
            this.lblTitle.Text = "🔍 Социальный сканер";

            this.lblUserLabel.AutoSize = true;
            this.lblUserLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUserLabel.ForeColor = textColor;
            this.lblUserLabel.Location = new System.Drawing.Point(15, 55);
            this.lblUserLabel.Text = "Пользователь:";

            this.comboBoxUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxUsers.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.comboBoxUsers.FormattingEnabled = true;
            this.comboBoxUsers.Location = new System.Drawing.Point(140, 50);
            this.comboBoxUsers.Size = new System.Drawing.Size(250, 25);
            this.comboBoxUsers.BackColor = bgColor;
            this.comboBoxUsers.ForeColor = textColor;
            this.comboBoxUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxUsers.SelectedIndexChanged += new System.EventHandler(this.ComboBoxUsers_SelectedIndexChanged);

            // Карточка пользователя
            this.panelUserCard.BackColor = bgColor;
            this.panelUserCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelUserCard.Location = new System.Drawing.Point(420, 45);
            this.panelUserCard.Size = new System.Drawing.Size(200, 45);
            this.panelUserCard.Controls.Add(this.lblUserName);
            this.panelUserCard.Controls.Add(this.lblUserFriends);

            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblUserName.ForeColor = accentPurple;
            this.lblUserName.Location = new System.Drawing.Point(10, 8);
            this.lblUserName.Text = "—";

            this.lblUserFriends.AutoSize = true;
            this.lblUserFriends.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUserFriends.ForeColor = mutedColor;
            this.lblUserFriends.Location = new System.Drawing.Point(10, 25);
            this.lblUserFriends.Text = "Друзей: —";

            // Глобальная статистика
            this.panelGlobalStats.BackColor = bgColor;
            this.panelGlobalStats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelGlobalStats.Location = new System.Drawing.Point(640, 45);
            this.panelGlobalStats.Size = new System.Drawing.Size(270, 45);
            this.panelGlobalStats.Controls.Add(this.lblAvgRecs);
            this.panelGlobalStats.Controls.Add(this.lblPercentRecs);
            this.panelGlobalStats.Controls.Add(this.progressBarRecs);

            this.lblAvgRecs.AutoSize = true;
            this.lblAvgRecs.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblAvgRecs.ForeColor = textColor;
            this.lblAvgRecs.Location = new System.Drawing.Point(10, 5);
            this.lblAvgRecs.Text = "Ср. рекомендаций: —";

            this.lblPercentRecs.AutoSize = true;
            this.lblPercentRecs.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPercentRecs.ForeColor = accentGreen;
            this.lblPercentRecs.Location = new System.Drawing.Point(10, 25);
            this.lblPercentRecs.Text = "Охват: —";

            this.progressBarRecs.Location = new System.Drawing.Point(150, 15);
            this.progressBarRecs.Size = new System.Drawing.Size(110, 20);
            this.progressBarRecs.Style = System.Windows.Forms.ProgressBarStyle.Continuous;

            // ===== ЛЕВАЯ ПАНЕЛЬ =====
            this.panelLeft.BackColor = panelColor;
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Width = 350;
            this.panelLeft.Controls.Add(this.lblRecsTitle);
            this.panelLeft.Controls.Add(this.dataGridViewRecs);

            this.lblRecsTitle.AutoSize = true;
            this.lblRecsTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblRecsTitle.ForeColor = accentGreen;
            this.lblRecsTitle.Location = new System.Drawing.Point(15, 10);
            this.lblRecsTitle.Text = " ТОП-10 кандидатов";

            this.dataGridViewRecs.AllowUserToAddRows = false;
            this.dataGridViewRecs.AllowUserToDeleteRows = false;
            this.dataGridViewRecs.AutoGenerateColumns = false;
            this.dataGridViewRecs.BackgroundColor = bgColor;
            this.dataGridViewRecs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewRecs.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridViewRecs.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridViewRecs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRecs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colRank, this.colCandidate, this.colMutual, this.colCoeff});
            this.dataGridViewRecs.Location = new System.Drawing.Point(15, 40);
            this.dataGridViewRecs.Size = new System.Drawing.Size(320, 490);
            this.dataGridViewRecs.EnableHeadersVisualStyles = false;
            this.dataGridViewRecs.ColumnHeadersDefaultCellStyle.BackColor = accentGreen;
            this.dataGridViewRecs.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            this.dataGridViewRecs.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dataGridViewRecs.ColumnHeadersHeight = 35;
            this.dataGridViewRecs.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(55, 55, 55);
            this.dataGridViewRecs.DefaultCellStyle.BackColor = bgColor;
            this.dataGridViewRecs.DefaultCellStyle.ForeColor = textColor;
            this.dataGridViewRecs.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dataGridViewRecs.DefaultCellStyle.Padding = new Padding(5);
            this.dataGridViewRecs.RowTemplate.Height = 35;
            this.dataGridViewRecs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewRecs.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewRecs_CellClick);

            this.colRank.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colRank.DataPropertyName = "Rank";
            this.colRank.HeaderText = "#";
            this.colRank.Name = "colRank";
            this.colRank.Width = 40;
            this.colRank.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;

            this.colCandidate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colCandidate.DataPropertyName = "Candidate";
            this.colCandidate.FillWeight = 100;
            this.colCandidate.HeaderText = "Кандидат";
            this.colCandidate.Name = "colCandidate";
            this.colCandidate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;

            this.colMutual.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colMutual.DataPropertyName = "MutualFriends";
            this.colMutual.HeaderText = "Общие";
            this.colMutual.Name = "colMutual";
            this.colMutual.Width = 80;
            this.colMutual.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;

            this.colCoeff.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colCoeff.DataPropertyName = "Coefficient";
            this.colCoeff.HeaderText = "Связность";
            this.colCoeff.Name = "colCoeff";
            this.colCoeff.Width = 90;
            this.colCoeff.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colCoeff.DefaultCellStyle.Format = "P1";

            // ===== ЦЕНТРАЛЬНАЯ ОБЛАСТЬ =====
            this.panelCenter.BackColor = bgColor;
            this.panelCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCenter.Controls.Add(this.pictureBoxGraph);
            this.panelCenter.Controls.Add(this.lblGraphInfo);
            this.panelCenter.Controls.Add(this.lblLoading);

            this.pictureBoxGraph.BackColor = bgColor;
            this.pictureBoxGraph.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxGraph.Location = new System.Drawing.Point(15, 15);
            this.pictureBoxGraph.Size = new System.Drawing.Size(500, 500);
            this.pictureBoxGraph.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxGraph.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureBoxGraph_Paint);
            this.pictureBoxGraph.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBoxGraph_MouseMove);

            this.lblGraphInfo.AutoSize = true;
            this.lblGraphInfo.Font = new System.Drawing.Font("Consolas", 8F);
            this.lblGraphInfo.ForeColor = mutedColor;
            this.lblGraphInfo.Location = new System.Drawing.Point(15, 520);
            this.lblGraphInfo.Text = "Алгоритм: BFS (глубина 2) | Вершин: 0";

            this.lblLoading.AutoSize = true;
            this.lblLoading.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Italic);
            this.lblLoading.ForeColor = accentBlue;
            this.lblLoading.Location = new System.Drawing.Point(200, 250);
            this.lblLoading.Text = "⏳ Анализ графа...";
            this.lblLoading.Visible = false;

            // ===== ПРАВАЯ ПАНЕЛЬ =====
            this.panelRight.BackColor = panelColor;
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRight.Width = 250;
            this.panelRight.Controls.Add(this.lblDetailsTitle);
            this.panelRight.Controls.Add(this.flowLayoutPanelCommonFriends);
            this.panelRight.Controls.Add(this.lblNoSelection);

            this.lblDetailsTitle.AutoSize = true;
            this.lblDetailsTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblDetailsTitle.ForeColor = accentBlue;
            this.lblDetailsTitle.Location = new System.Drawing.Point(15, 10);
            this.lblDetailsTitle.Text = "👥 Общие друзья";

            this.flowLayoutPanelCommonFriends.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelCommonFriends.Location = new System.Drawing.Point(15, 45);
            this.flowLayoutPanelCommonFriends.Size = new System.Drawing.Size(220, 485);
            this.flowLayoutPanelCommonFriends.AutoScroll = true;

            this.lblNoSelection.AutoSize = true;
            this.lblNoSelection.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNoSelection.ForeColor = mutedColor;
            this.lblNoSelection.Location = new System.Drawing.Point(15, 50);
            this.lblNoSelection.Text = "Выберите кандидата\nдля просмотра общих друзей";

            // ===== НИЖНЯЯ ПАНЕЛЬ =====
            this.panelBottom.BackColor = panelColor;
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Height = 40;
            this.panelBottom.Controls.Add(this.lblStatus);
            this.panelBottom.Controls.Add(this.lblResults);

            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.ForeColor = accentGreen;
            this.lblStatus.Location = new System.Drawing.Point(15, 12);
            this.lblStatus.Text = "✓ Готов к работе";

            this.lblResults.AutoSize = true;
            this.lblResults.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblResults.ForeColor = textColor;
            this.lblResults.Location = new System.Drawing.Point(300, 12);
            this.lblResults.Text = "—";

            // ===== ФОРМА =====
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = bgColor;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.panelCenter);
            this.Controls.Add(this.panelRight);
            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.MinimumSize = new System.Drawing.Size(1200, 700);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Социальный сканер | Рекомендательная система друзей";
            this.Load += new System.EventHandler(this.Form1_Load);

            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelUserCard.ResumeLayout(false);
            this.panelUserCard.PerformLayout();
            this.panelGlobalStats.ResumeLayout(false);
            this.panelGlobalStats.PerformLayout();
            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRecs)).EndInit();
            this.panelCenter.ResumeLayout(false);
            this.panelCenter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGraph)).EndInit();
            this.panelRight.ResumeLayout(false);
            this.panelRight.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.ResumeLayout(false);
        }

        // ===== ОБЪЯВЛЕНИЕ ПОЛЕЙ =====
        private System.Windows.Forms.Panel panelTop, panelLeft, panelCenter, panelRight, panelBottom;
        private System.Windows.Forms.Label lblTitle, lblUserLabel, lblUserName, lblUserFriends;
        private System.Windows.Forms.Label lblAvgRecs, lblPercentRecs, lblRecsTitle, lblGraphInfo, lblLoading;
        private System.Windows.Forms.Label lblDetailsTitle, lblNoSelection, lblStatus, lblResults;
        private System.Windows.Forms.ComboBox comboBoxUsers;
        private System.Windows.Forms.Panel panelUserCard, panelGlobalStats;
        private System.Windows.Forms.ProgressBar progressBarRecs;
        private System.Windows.Forms.DataGridView dataGridViewRecs;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRank, colCandidate, colMutual, colCoeff;
        private System.Windows.Forms.PictureBox pictureBoxGraph;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelCommonFriends;
    }
}