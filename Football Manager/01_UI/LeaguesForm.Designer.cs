namespace Football_Manager.UI
{
    partial class LeaguesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            dgvLeagues = new DataGridView();
            colLeagueName = new DataGridViewTextBoxColumn();
            colLeagueSeason = new DataGridViewTextBoxColumn();
            txtName = new TextBox();
            txtSeason = new TextBox();
            dgvParticipants = new DataGridView();
            colPartName = new DataGridViewTextBoxColumn();
            colPartCity = new DataGridViewTextBoxColumn();
            cboAvailableClubs = new ComboBox();
            btnAddClub = new Button();
            btnRemoveClub = new Button();
            btnAddLeague = new Button();
            label1 = new Label();
            label2 = new Label();
            btnUpdateLeague = new Button();
            btnClear = new Button();
            label3 = new Label();
            btnDeleteLeague = new Button();
            lineSeparator = new Panel();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            dgvSchedule = new DataGridView();
            colMatchRound = new DataGridViewTextBoxColumn();
            colHomeTeam = new DataGridViewTextBoxColumn();
            colAwayTeam = new DataGridViewTextBoxColumn();
            colHomeScore = new DataGridViewTextBoxColumn();
            colAwayScore = new DataGridViewTextBoxColumn();
            colMatchDate = new DataGridViewTextBoxColumn();
            colIsPlayed = new DataGridViewCheckBoxColumn();
            btnGenerateSchedule = new Button();
            panel1 = new Panel();
            btnManageMatch = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvLeagues).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvParticipants).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvSchedule).BeginInit();
            SuspendLayout();
            // 
            // dgvLeagues
            // 
            dgvLeagues.AllowUserToAddRows = false;
            dgvLeagues.AllowUserToDeleteRows = false;
            dgvLeagues.AllowUserToResizeColumns = false;
            dgvLeagues.AllowUserToResizeRows = false;
            dgvLeagues.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvLeagues.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvLeagues.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLeagues.Columns.AddRange(new DataGridViewColumn[] { colLeagueName, colLeagueSeason });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvLeagues.DefaultCellStyle = dataGridViewCellStyle2;
            dgvLeagues.Location = new Point(26, 32);
            dgvLeagues.MultiSelect = false;
            dgvLeagues.Name = "dgvLeagues";
            dgvLeagues.ReadOnly = true;
            dgvLeagues.RowHeadersVisible = false;
            dgvLeagues.RowTemplate.Height = 30;
            dgvLeagues.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLeagues.Size = new Size(339, 459);
            dgvLeagues.TabIndex = 0;
            dgvLeagues.CellClick += dgvLeagues_CellClick;
            // 
            // colLeagueName
            // 
            colLeagueName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colLeagueName.DataPropertyName = "name";
            colLeagueName.FillWeight = 98.47716F;
            colLeagueName.HeaderText = "Лига";
            colLeagueName.Name = "colLeagueName";
            colLeagueName.ReadOnly = true;
            // 
            // colLeagueSeason
            // 
            colLeagueSeason.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colLeagueSeason.DataPropertyName = "season";
            colLeagueSeason.HeaderText = "Сезон";
            colLeagueSeason.Name = "colLeagueSeason";
            colLeagueSeason.ReadOnly = true;
            colLeagueSeason.Width = 150;
            // 
            // txtName
            // 
            txtName.Font = new Font("Segoe UI", 14.25F);
            txtName.Location = new Point(26, 524);
            txtName.Name = "txtName";
            txtName.Size = new Size(261, 33);
            txtName.TabIndex = 1;
            // 
            // txtSeason
            // 
            txtSeason.Font = new Font("Segoe UI", 14.25F);
            txtSeason.Location = new Point(26, 593);
            txtSeason.Name = "txtSeason";
            txtSeason.Size = new Size(137, 33);
            txtSeason.TabIndex = 1;
            // 
            // dgvParticipants
            // 
            dgvParticipants.AllowUserToAddRows = false;
            dgvParticipants.AllowUserToDeleteRows = false;
            dgvParticipants.AllowUserToResizeColumns = false;
            dgvParticipants.AllowUserToResizeRows = false;
            dgvParticipants.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvParticipants.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvParticipants.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvParticipants.Columns.AddRange(new DataGridViewColumn[] { colPartName, colPartCity });
            dgvParticipants.DefaultCellStyle = dataGridViewCellStyle2;
            dgvParticipants.Location = new Point(427, 32);
            dgvParticipants.MultiSelect = false;
            dgvParticipants.Name = "dgvParticipants";
            dgvParticipants.ReadOnly = true;
            dgvParticipants.RowHeadersVisible = false;
            dgvParticipants.RowTemplate.Height = 30;
            dgvParticipants.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvParticipants.Size = new Size(341, 459);
            dgvParticipants.TabIndex = 0;
            // 
            // colPartName
            // 
            colPartName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colPartName.DataPropertyName = "name";
            colPartName.HeaderText = "Отбор";
            colPartName.Name = "colPartName";
            colPartName.ReadOnly = true;
            // 
            // colPartCity
            // 
            colPartCity.DataPropertyName = "city";
            colPartCity.HeaderText = "Град";
            colPartCity.Name = "colPartCity";
            colPartCity.ReadOnly = true;
            // 
            // cboAvailableClubs
            // 
            cboAvailableClubs.Font = new Font("Segoe UI", 14.25F);
            cboAvailableClubs.FormattingEnabled = true;
            cboAvailableClubs.Location = new Point(468, 538);
            cboAvailableClubs.Name = "cboAvailableClubs";
            cboAvailableClubs.Size = new Size(251, 33);
            cboAvailableClubs.TabIndex = 2;
            // 
            // btnAddClub
            // 
            btnAddClub.BackColor = Color.LightGreen;
            btnAddClub.Font = new Font("Segoe UI", 14.25F);
            btnAddClub.Location = new Point(442, 614);
            btnAddClub.Name = "btnAddClub";
            btnAddClub.Size = new Size(144, 50);
            btnAddClub.TabIndex = 3;
            btnAddClub.Text = "Добави клуб";
            btnAddClub.UseVisualStyleBackColor = false;
            btnAddClub.Click += btnAddClub_Click;
            // 
            // btnRemoveClub
            // 
            btnRemoveClub.BackColor = Color.Coral;
            btnRemoveClub.Font = new Font("Segoe UI", 14.25F);
            btnRemoveClub.Location = new Point(624, 614);
            btnRemoveClub.Name = "btnRemoveClub";
            btnRemoveClub.Size = new Size(144, 50);
            btnRemoveClub.TabIndex = 3;
            btnRemoveClub.Text = "Махни клуб";
            btnRemoveClub.UseVisualStyleBackColor = false;
            btnRemoveClub.Click += btnRemoveClub_Click;
            // 
            // btnAddLeague
            // 
            btnAddLeague.BackColor = Color.LightGreen;
            btnAddLeague.Font = new Font("Segoe UI", 14.25F);
            btnAddLeague.Location = new Point(24, 637);
            btnAddLeague.Name = "btnAddLeague";
            btnAddLeague.Size = new Size(144, 50);
            btnAddLeague.TabIndex = 4;
            btnAddLeague.Text = "Добави Лига";
            btnAddLeague.UseVisualStyleBackColor = false;
            btnAddLeague.Click += btnAddLeague_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F);
            label1.Location = new Point(26, 496);
            label1.Name = "label1";
            label1.Size = new Size(118, 25);
            label1.TabIndex = 5;
            label1.Text = "Име на лига";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F);
            label2.Location = new Point(26, 565);
            label2.Name = "label2";
            label2.Size = new Size(231, 25);
            label2.TabIndex = 5;
            label2.Text = "Сезон (Формат:ГГГГ/ГГГГ)";
            // 
            // btnUpdateLeague
            // 
            btnUpdateLeague.BackColor = Color.LightBlue;
            btnUpdateLeague.Font = new Font("Segoe UI", 14.25F);
            btnUpdateLeague.Location = new Point(221, 637);
            btnUpdateLeague.Name = "btnUpdateLeague";
            btnUpdateLeague.Size = new Size(144, 50);
            btnUpdateLeague.TabIndex = 4;
            btnUpdateLeague.Text = "Обнови";
            btnUpdateLeague.UseVisualStyleBackColor = false;
            btnUpdateLeague.Click += btnUpdateLeague_Click;
            // 
            // btnClear
            // 
            btnClear.Font = new Font("Segoe UI", 14.25F);
            btnClear.Location = new Point(221, 703);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(144, 50);
            btnClear.TabIndex = 4;
            btnClear.Text = "Изчисти";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14.25F);
            label3.Location = new Point(471, 510);
            label3.Name = "label3";
            label3.Size = new Size(248, 25);
            label3.TabIndex = 5;
            label3.Text = "Избор на клуб за добавяне";
            // 
            // btnDeleteLeague
            // 
            btnDeleteLeague.BackColor = Color.Coral;
            btnDeleteLeague.Font = new Font("Segoe UI", 14.25F);
            btnDeleteLeague.Location = new Point(24, 703);
            btnDeleteLeague.Name = "btnDeleteLeague";
            btnDeleteLeague.Size = new Size(144, 50);
            btnDeleteLeague.TabIndex = 4;
            btnDeleteLeague.Text = "Изтрий Лига";
            btnDeleteLeague.UseVisualStyleBackColor = false;
            btnDeleteLeague.Click += btnDeleteLeague_Click;
            // 
            // lineSeparator
            // 
            lineSeparator.BackColor = Color.LightGray;
            lineSeparator.Location = new Point(402, 44);
            lineSeparator.Name = "lineSeparator";
            lineSeparator.Size = new Size(1, 700);
            lineSeparator.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold);
            label4.Location = new Point(157, 4);
            label4.Name = "label4";
            label4.Size = new Size(58, 25);
            label4.TabIndex = 7;
            label4.Text = "Лиги";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold);
            label5.Location = new Point(546, 4);
            label5.Name = "label5";
            label5.Size = new Size(111, 25);
            label5.TabIndex = 7;
            label5.Text = "Участници";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold);
            label6.Location = new Point(1138, 4);
            label6.Name = "label6";
            label6.Size = new Size(81, 25);
            label6.TabIndex = 7;
            label6.Text = "График";
            // 
            // dgvSchedule
            // 
            dgvSchedule.AllowUserToAddRows = false;
            dgvSchedule.AllowUserToDeleteRows = false;
            dgvSchedule.AllowUserToResizeColumns = false;
            dgvSchedule.AllowUserToResizeRows = false;
            dgvSchedule.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvSchedule.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvSchedule.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSchedule.Columns.AddRange(new DataGridViewColumn[] { colMatchRound, colHomeTeam, colAwayTeam, colHomeScore, colAwayScore, colMatchDate, colIsPlayed });
            dgvSchedule.DefaultCellStyle = dataGridViewCellStyle2;
            dgvSchedule.Location = new Point(807, 32);
            dgvSchedule.MultiSelect = false;
            dgvSchedule.Name = "dgvSchedule";
            dgvSchedule.ReadOnly = true;
            dgvSchedule.RowHeadersVisible = false;
            dgvSchedule.RowTemplate.Height = 30;
            dgvSchedule.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSchedule.Size = new Size(708, 459);
            dgvSchedule.TabIndex = 0;
            dgvSchedule.CellDoubleClick += dgvSchedule_CellDoubleClick;
            // 
            // colMatchRound
            // 
            colMatchRound.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colMatchRound.DataPropertyName = "round_no";
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colMatchRound.DefaultCellStyle = dataGridViewCellStyle4;
            colMatchRound.HeaderText = "Кръг";
            colMatchRound.Name = "colMatchRound";
            colMatchRound.ReadOnly = true;
            colMatchRound.Width = 50;
            // 
            // colHomeTeam
            // 
            colHomeTeam.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colHomeTeam.DataPropertyName = "home_team";
            colHomeTeam.HeaderText = "Домакин";
            colHomeTeam.Name = "colHomeTeam";
            colHomeTeam.ReadOnly = true;
            // 
            // colAwayTeam
            // 
            colAwayTeam.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colAwayTeam.DataPropertyName = "away_team";
            colAwayTeam.HeaderText = "Гост";
            colAwayTeam.Name = "colAwayTeam";
            colAwayTeam.ReadOnly = true;
            // 
            // colHomeScore
            // 
            colHomeScore.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colHomeScore.DataPropertyName = "home_score";
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colHomeScore.DefaultCellStyle = dataGridViewCellStyle5;
            colHomeScore.HeaderText = "ГД";
            colHomeScore.Name = "colHomeScore";
            colHomeScore.ReadOnly = true;
            colHomeScore.Width = 40;
            // 
            // colAwayScore
            // 
            colAwayScore.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colAwayScore.DataPropertyName = "away_score";
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colAwayScore.DefaultCellStyle = dataGridViewCellStyle6;
            colAwayScore.HeaderText = "ГГ";
            colAwayScore.Name = "colAwayScore";
            colAwayScore.ReadOnly = true;
            colAwayScore.Width = 40;
            // 
            // colMatchDate
            // 
            colMatchDate.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colMatchDate.DataPropertyName = "match_date";
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colMatchDate.DefaultCellStyle = dataGridViewCellStyle7;
            colMatchDate.HeaderText = "Дата";
            colMatchDate.Name = "colMatchDate";
            colMatchDate.ReadOnly = true;
            // 
            // colIsPlayed
            // 
            colIsPlayed.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colIsPlayed.DataPropertyName = "is_played";
            colIsPlayed.HeaderText = "Изигран";
            colIsPlayed.Name = "colIsPlayed";
            colIsPlayed.ReadOnly = true;
            colIsPlayed.Width = 80;
            // 
            // btnGenerateSchedule
            // 
            btnGenerateSchedule.BackColor = Color.LightGreen;
            btnGenerateSchedule.Font = new Font("Segoe UI", 14.25F);
            btnGenerateSchedule.Location = new Point(1058, 538);
            btnGenerateSchedule.Name = "btnGenerateSchedule";
            btnGenerateSchedule.Size = new Size(195, 50);
            btnGenerateSchedule.TabIndex = 8;
            btnGenerateSchedule.Text = "Генерирай график";
            btnGenerateSchedule.UseVisualStyleBackColor = false;
            btnGenerateSchedule.Click += btnGenerateSchedule_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.LightGray;
            panel1.Location = new Point(789, 44);
            panel1.Name = "panel1";
            panel1.Size = new Size(1, 700);
            panel1.TabIndex = 6;
            // 
            // btnManageMatch
            // 
            btnManageMatch.BackColor = Color.LightGreen;
            btnManageMatch.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnManageMatch.Location = new Point(1028, 635);
            btnManageMatch.Name = "btnManageMatch";
            btnManageMatch.Size = new Size(251, 52);
            btnManageMatch.TabIndex = 9;
            btnManageMatch.Text = "Управление на мач";
            btnManageMatch.UseVisualStyleBackColor = false;
            btnManageMatch.Click += btnManageMatch_Click;
            // 
            // LeaguesForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1527, 770);
            Controls.Add(btnManageMatch);
            Controls.Add(btnGenerateSchedule);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(panel1);
            Controls.Add(lineSeparator);
            Controls.Add(dgvLeagues);
            Controls.Add(dgvSchedule);
            Controls.Add(dgvParticipants);
            Controls.Add(txtName);
            Controls.Add(cboAvailableClubs);
            Controls.Add(label3);
            Controls.Add(btnAddClub);
            Controls.Add(txtSeason);
            Controls.Add(btnRemoveClub);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnAddLeague);
            Controls.Add(btnUpdateLeague);
            Controls.Add(btnClear);
            Controls.Add(btnDeleteLeague);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "LeaguesForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Управление на лиги";
            Load += LeaguesForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvLeagues).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvParticipants).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvSchedule).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvLeagues;
        private TextBox txtName;
        private TextBox txtSeason;
        private DataGridView dgvParticipants;
        private ComboBox cboAvailableClubs;
        private Button btnAddClub;
        private Button btnRemoveClub;
        private Button btnAddLeague;
        private Label label1;
        private Label label2;
        private Button btnUpdateLeague;
        private Button btnClear;
        private Label label3;
        private Button btnDeleteLeague;
        private Panel lineSeparator;
        private Label label4;
        private Label label5;
        private Label label6;
        private DataGridView dgvSchedule;
        private Button btnGenerateSchedule;
        private Panel panel1;
        private Button btnManageMatch;
        private DataGridViewTextBoxColumn colLeagueName;
        private DataGridViewTextBoxColumn colLeagueSeason;
        private DataGridViewTextBoxColumn colPartName;
        private DataGridViewTextBoxColumn colPartCity;
        private DataGridViewTextBoxColumn colMatchRound;
        private DataGridViewTextBoxColumn colHomeTeam;
        private DataGridViewTextBoxColumn colAwayTeam;
        private DataGridViewTextBoxColumn colHomeScore;
        private DataGridViewTextBoxColumn colAwayScore;
        private DataGridViewTextBoxColumn colMatchDate;
        private DataGridViewCheckBoxColumn colIsPlayed;
    }
}