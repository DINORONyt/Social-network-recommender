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
            this.groupBoxControls = new System.Windows.Forms.GroupBox();
            this.comboBoxUsers = new System.Windows.Forms.ComboBox();
            this.btnGenerateRecs = new System.Windows.Forms.Button();
            this.btnStats = new System.Windows.Forms.Button();
            this.labelUser = new System.Windows.Forms.Label();

            this.groupBoxRecs = new System.Windows.Forms.GroupBox();
            this.dataGridViewRecs = new System.Windows.Forms.DataGridView();
            this.colCandidate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMutual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCoeff = new System.Windows.Forms.DataGridViewTextBoxColumn();

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

            // groupBoxControls
            this.groupBoxControls.Controls.Add(this.btnStats);
            this.groupBoxControls.Controls.Add(this.labelUser);
            this.groupBoxControls.Controls.Add(this.comboBoxUsers);
            this.groupBoxControls.Controls.Add(this.btnGenerateRecs);
            this.groupBoxControls.Location = new System.Drawing.Point(12, 12);
            this.groupBoxControls.Name = "groupBoxControls";
            this.groupBoxControls.Size = new System.Drawing.Size(320, 100);
            this.groupBoxControls.TabIndex = 0;
            this.groupBoxControls.TabStop = false;
            this.groupBoxControls.Text = "Управление";

            // labelUser
            this.labelUser.AutoSize = true;
            this.labelUser.Location = new System.Drawing.Point(15, 25);
            this.labelUser.Text = "Выберите пользователя:";

            // comboBoxUsers
            this.comboBoxUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxUsers.Location = new System.Drawing.Point(15, 45);
            this.comboBoxUsers.Name = "comboBoxUsers";
            this.comboBoxUsers.Size = new System.Drawing.Size(180, 23);

            // btnGenerateRecs
            this.btnGenerateRecs.Location = new System.Drawing.Point(205, 42);
            this.btnGenerateRecs.Name = "btnGenerateRecs";
            this.btnGenerateRecs.Size = new System.Drawing.Size(100, 25);
            this.btnGenerateRecs.Text = "Найти друзей";
            this.btnGenerateRecs.Click += new System.EventHandler(this.btnGenerateRecs_Click);

            // btnStats
            this.btnStats.Location = new System.Drawing.Point(205, 70);
            this.btnStats.Name = "btnStats";
            this.btnStats.Size = new System.Drawing.Size(100, 25);
            this.btnStats.Text = "Статистика сети";
            this.btnStats.Click += new System.EventHandler(this.btnStats_Click);

            // groupBoxRecs
            this.groupBoxRecs.Controls.Add(this.dataGridViewRecs);
            this.groupBoxRecs.Location = new System.Drawing.Point(12, 118);
            this.groupBoxRecs.Name = "groupBoxRecs";
            this.groupBoxRecs.Size = new System.Drawing.Size(320, 250);
            this.groupBoxRecs.Text = "Топ-10 рекомендаций";

            // dataGridViewRecs
            this.dataGridViewRecs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colCandidate, this.colMutual, this.colCoeff});
            this.dataGridViewRecs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewRecs.ReadOnly = true;
            this.dataGridViewRecs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            // colCandidate
            this.colCandidate.HeaderText = "Кандидат"; this.colCandidate.Name = "colCandidate";
            // colMutual
            this.colMutual.HeaderText = "Общие друзья"; this.colMutual.Name = "colMutual";
            // colCoeff
            this.colCoeff.HeaderText = "Коэф. связности"; this.colCoeff.Name = "colCoeff";

            // groupBoxVisual
            this.groupBoxVisual.Controls.Add(this.pictureBoxNetwork);
            this.groupBoxVisual.Location = new System.Drawing.Point(338, 12);
            this.groupBoxVisual.Name = "groupBoxVisual";
            this.groupBoxVisual.Size = new System.Drawing.Size(450, 356);
            this.groupBoxVisual.Text = "Визуализация сети";

            // pictureBoxNetwork
            this.pictureBoxNetwork.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxNetwork.BackColor = System.Drawing.Color.White;
            this.pictureBoxNetwork.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxNetwork_Paint);

            // groupBoxStats
            this.groupBoxStats.Controls.Add(this.lblStats);
            this.groupBoxStats.Location = new System.Drawing.Point(338, 374);
            this.groupBoxStats.Name = "groupBoxStats";
            this.groupBoxStats.Size = new System.Drawing.Size(450, 100);
            this.groupBoxStats.Text = "Глобальная статистика";

            // lblStats
            this.lblStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStats.Font = new System.Drawing.Font("Consolas", 9F);
            this.lblStats.Text = "Нажмите 'Статистика сети' для расчёта.";

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 485);
            this.Controls.Add(this.groupBoxStats);
            this.Controls.Add(this.groupBoxVisual);
            this.Controls.Add(this.groupBoxRecs);
            this.Controls.Add(this.groupBoxControls);
            this.Name = "Form1";
            this.Text = "Рекомендательная система друзей (Тема 20)";
            this.Load += new System.EventHandler(this.Form1_Load);

            this.groupBoxControls.ResumeLayout(false);
            this.groupBoxControls.PerformLayout();
            this.groupBoxRecs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRecs)).EndInit();
            this.groupBoxVisual.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNetwork)).EndInit();
            this.groupBoxStats.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.GroupBox groupBoxControls, groupBoxRecs, groupBoxVisual, groupBoxStats;
        private System.Windows.Forms.ComboBox comboBoxUsers;
        private System.Windows.Forms.Button btnGenerateRecs, btnStats;
        private System.Windows.Forms.Label labelUser, lblStats;
        private System.Windows.Forms.DataGridView dataGridViewRecs;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCandidate, colMutual, colCoeff;
        private System.Windows.Forms.PictureBox pictureBoxNetwork;
    }
}