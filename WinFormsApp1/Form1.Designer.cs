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

            Color primaryColor = Color.FromArgb(0, 102, 204);
            Color secondaryColor = Color.FromArgb(0, 153, 204);
            Color accentColor = Color.FromArgb(0, 204, 153);
            Color backgroundColor = Color.FromArgb(245, 247, 250);
            Color panelColor = Color.White;

            this.groupBoxControls = new System.Windows.Forms.GroupBox();
            this.comboBoxUsers = new System.Windows.Forms.ComboBox();
            this.btnGenerateRecs = new System.Windows.Forms.Button();
            this.btnStats = new System.Windows.Forms.Button();
            this.labelUser = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();

            this.groupBoxRecs = new System.Windows.Forms.GroupBox();
            this.dataGridViewRecs = new System.Windows.Forms.DataGridView();
            this.colRank = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCandidate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMutual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCoeff = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFriends = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.groupBoxVisual = new System.Windows.Forms.GroupBox();
            this.pictureBoxNetwork = new System.Windows.Forms.PictureBox();

            this.groupBoxStats = new System.Windows.Forms.GroupBox();
            this.lblStats = new System.Windows.Forms.Label();

            this.groupBoxControls.SuspendLayout();
            this.groupBoxRecs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRecs)).BeginInit();
            this.groupBoxVisual.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNetwork)).BeginInit();
            this.groupBoxStats.SuspendLayout();
            this.SuspendLayout();

            // labelTitle
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = primaryColor;
            this.labelTitle.Location = new System.Drawing.Point(12, 9);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(520, 37);
            this.labelTitle.Text = "Рекомендательная система друзей";

            // groupBoxControls - ИСПРАВЛЕНО: убраны FlatAppearance
            this.groupBoxControls.BackColor = panelColor;
            this.groupBoxControls.Controls.Add(this.btnStats);
            this.groupBoxControls.Controls.Add(this.labelUser);
            this.groupBoxControls.Controls.Add(this.comboBoxUsers);
            this.groupBoxControls.Controls.Add(this.btnGenerateRecs);
            this.groupBoxControls.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupBoxControls.ForeColor = primaryColor;
            this.groupBoxControls.Location = new System.Drawing.Point(12, 55);
            this.groupBoxControls.Name = "groupBoxControls";
            this.groupBoxControls.Size = new System.Drawing.Size(380, 120);
            this.groupBoxControls.TabIndex = 0;
            this.groupBoxControls.TabStop = false;
            this.groupBoxControls.Text = "Управление";

            // labelUser
            this.labelUser.AutoSize = true;
            this.labelUser.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.labelUser.ForeColor = Color.FromArgb(64, 64, 64);
            this.labelUser.Location = new System.Drawing.Point(18, 32);
            this.labelUser.Name = "labelUser";
            this.labelUser.Size = new System.Drawing.Size(195, 17);
            this.labelUser.Text = "Выберите пользователя из списка:";

            // comboBoxUsers
            this.comboBoxUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxUsers.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.comboBoxUsers.FormattingEnabled = true;
            this.comboBoxUsers.Location = new System.Drawing.Point(21, 55);
            this.comboBoxUsers.Name = "comboBoxUsers";
            this.comboBoxUsers.Size = new System.Drawing.Size(220, 25);
            this.comboBoxUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxUsers.BackColor = Color.White;

            // btnGenerateRecs
            this.btnGenerateRecs.BackColor = primaryColor;
            this.btnGenerateRecs.FlatAppearance.BorderSize = 0;
            this.btnGenerateRecs.FlatAppearance.MouseOverBackColor = secondaryColor;
            this.btnGenerateRecs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerateRecs.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnGenerateRecs.ForeColor = Color.White;
            this.btnGenerateRecs.Location = new System.Drawing.Point(250, 52);
            this.btnGenerateRecs.Name = "btnGenerateRecs";
            this.btnGenerateRecs.Size = new System.Drawing.Size(120, 30);
            this.btnGenerateRecs.TabIndex = 1;
            this.btnGenerateRecs.Text = "Найти друзей";
            this.btnGenerateRecs.UseVisualStyleBackColor = false;
            this.btnGenerateRecs.Click += new System.EventHandler(this.btnGenerateRecs_Click);

            // btnStats
            this.btnStats.BackColor = accentColor;
            this.btnStats.FlatAppearance.BorderSize = 0;
            this.btnStats.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 220, 170);
            this.btnStats.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStats.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnStats.ForeColor = Color.White;
            this.btnStats.Location = new System.Drawing.Point(250, 85);
            this.btnStats.Name = "btnStats";
            this.btnStats.Size = new System.Drawing.Size(120, 30);
            this.btnStats.TabIndex = 2;
            this.btnStats.Text = "Статистика сети";
            this.btnStats.UseVisualStyleBackColor = false;
            this.btnStats.Click += new System.EventHandler(this.btnStats_Click);

            // groupBoxRecs - ИСПРАВЛЕНО: убраны FlatAppearance
            this.groupBoxRecs.BackColor = panelColor;
            this.groupBoxRecs.Controls.Add(this.dataGridViewRecs);
            this.groupBoxRecs.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupBoxRecs.ForeColor = primaryColor;
            this.groupBoxRecs.Location = new System.Drawing.Point(12, 181);
            this.groupBoxRecs.Name = "groupBoxRecs";
            this.groupBoxRecs.Size = new System.Drawing.Size(380, 420);
            this.groupBoxRecs.TabIndex = 1;
            this.groupBoxRecs.TabStop = false;
            this.groupBoxRecs.Text = "Топ-10 рекомендаций";

            // dataGridViewRecs
            this.dataGridViewRecs.AllowUserToAddRows = false;
            this.dataGridViewRecs.AllowUserToDeleteRows = false;
            this.dataGridViewRecs.BackgroundColor = Color.White;
            this.dataGridViewRecs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewRecs.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridViewRecs.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridViewRecs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRecs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colRank,
                this.colCandidate,
                this.colMutual,
                this.colCoeff,
                this.colFriends});
            this.dataGridViewRecs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewRecs.EnableHeadersVisualStyles = false;
            this.dataGridViewRecs.GridColor = Color.FromArgb(230, 235, 240);
            this.dataGridViewRecs.Location = new System.Drawing.Point(3, 21);
            this.dataGridViewRecs.MultiSelect = false;
            this.dataGridViewRecs.Name = "dataGridViewRecs";
            this.dataGridViewRecs.ReadOnly = true;
            this.dataGridViewRecs.RowHeadersVisible = false;
            this.dataGridViewRecs.RowTemplate.Height = 30;
            this.dataGridViewRecs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewRecs.Size = new System.Drawing.Size(374, 396);
            this.dataGridViewRecs.TabIndex = 0;

            this.dataGridViewRecs.ColumnHeadersDefaultCellStyle.BackColor = primaryColor;
            this.dataGridViewRecs.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dataGridViewRecs.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dataGridViewRecs.ColumnHeadersHeight = 35;
            this.dataGridViewRecs.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 252, 255);
            this.dataGridViewRecs.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dataGridViewRecs.DefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);

            // colRank
            this.colRank.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colRank.HeaderText = "№";
            this.colRank.Name = "colRank";
            this.colRank.Width = 40;
            this.colRank.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;

            // colCandidate
            this.colCandidate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colCandidate.HeaderText = "Рекомендуемый друг";
            this.colCandidate.Name = "colCandidate";
            this.colCandidate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);

            // colMutual
            this.colMutual.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colMutual.HeaderText = "Общих друзей";
            this.colMutual.Name = "colMutual";
            this.colMutual.Width = 90;
            this.colMutual.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;

            // colCoeff
            this.colCoeff.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colCoeff.HeaderText = "Связность";
            this.colCoeff.Name = "colCoeff";
            this.colCoeff.Width = 80;
            this.colCoeff.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colCoeff.DefaultCellStyle.Format = "P1";

            // colFriends
            this.colFriends.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colFriends.HeaderText = "Всего друзей";
            this.colFriends.Name = "colFriends";
            this.colFriends.Width = 85;
            this.colFriends.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;

            // groupBoxVisual - ИСПРАВЛЕНО: убраны FlatAppearance
            this.groupBoxVisual.BackColor = panelColor;
            this.groupBoxVisual.Controls.Add(this.pictureBoxNetwork);
            this.groupBoxVisual.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupBoxVisual.ForeColor = primaryColor;
            this.groupBoxVisual.Location = new System.Drawing.Point(398, 55);
            this.groupBoxVisual.Name = "groupBoxVisual";
            this.groupBoxVisual.Size = new System.Drawing.Size(520, 380);
            this.groupBoxVisual.TabIndex = 2;
            this.groupBoxVisual.TabStop = false;
            this.groupBoxVisual.Text = "Визуализация социальной сети";

            // pictureBoxNetwork
            this.pictureBoxNetwork.BackColor = Color.White;
            this.pictureBoxNetwork.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxNetwork.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxNetwork.Location = new System.Drawing.Point(3, 21);
            this.pictureBoxNetwork.Name = "pictureBoxNetwork";
            this.pictureBoxNetwork.Size = new System.Drawing.Size(514, 356);
            this.pictureBoxNetwork.TabIndex = 0;
            this.pictureBoxNetwork.TabStop = false;
            this.pictureBoxNetwork.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxNetwork_Paint);

            // groupBoxStats - ИСПРАВЛЕНО: убраны FlatAppearance
            this.groupBoxStats.BackColor = panelColor;
            this.groupBoxStats.Controls.Add(this.lblStats);
            this.groupBoxStats.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupBoxStats.ForeColor = primaryColor;
            this.groupBoxStats.Location = new System.Drawing.Point(398, 441);
            this.groupBoxStats.Name = "groupBoxStats";
            this.groupBoxStats.Size = new System.Drawing.Size(520, 160);
            this.groupBoxStats.TabIndex = 3;
            this.groupBoxStats.TabStop = false;
            this.groupBoxStats.Text = "Глобальная статистика сети";

            // lblStats
            this.lblStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStats.Font = new System.Drawing.Font("Consolas", 9.5F, System.Drawing.FontStyle.Regular);
            this.lblStats.ForeColor = Color.FromArgb(64, 64, 64);
            this.lblStats.Location = new System.Drawing.Point(3, 21);
            this.lblStats.Name = "lblStats";
            this.lblStats.Size = new System.Drawing.Size(514, 136);
            this.lblStats.TabIndex = 0;
            this.lblStats.Text = "Нажмите кнопку \"Статистика сети\" для расчёта показателей социальной сети...";
            this.lblStats.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblStats.Padding = new Padding(10);

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = backgroundColor;
            this.ClientSize = new System.Drawing.Size(930, 613);
            this.Controls.Add(this.groupBoxStats);
            this.Controls.Add(this.groupBoxVisual);
            this.Controls.Add(this.groupBoxRecs);
            this.Controls.Add(this.groupBoxControls);
            this.Controls.Add(this.labelTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Рекомендательная система друзей | Социальная сеть";
            this.Load += new System.EventHandler(this.Form1_Load);

            this.groupBoxControls.ResumeLayout(false);
            this.groupBoxControls.PerformLayout();
            this.groupBoxRecs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRecs)).EndInit();
            this.groupBoxVisual.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNetwork)).EndInit();
            this.groupBoxStats.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.GroupBox groupBoxControls, groupBoxRecs, groupBoxVisual, groupBoxStats;
        private System.Windows.Forms.ComboBox comboBoxUsers;
        private System.Windows.Forms.Button btnGenerateRecs, btnStats;
        private System.Windows.Forms.Label labelUser, labelTitle, lblStats;
        private System.Windows.Forms.DataGridView dataGridViewRecs;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRank, colCandidate, colMutual, colCoeff, colFriends;
        private System.Windows.Forms.PictureBox pictureBoxNetwork;
    }
}