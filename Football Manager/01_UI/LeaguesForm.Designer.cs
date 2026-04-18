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
            dgvLeagues = new DataGridView();
            txtName = new TextBox();
            txtSeason = new TextBox();
            dgvParticipants = new DataGridView();
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
            ((System.ComponentModel.ISupportInitialize)dgvLeagues).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvParticipants).BeginInit();
            SuspendLayout();
            // 
            // dgvLeagues
            // 
            dgvLeagues.AllowUserToAddRows = false;
            dgvLeagues.AllowUserToDeleteRows = false;
            dgvLeagues.AllowUserToResizeColumns = false;
            dgvLeagues.AllowUserToResizeRows = false;
            dgvLeagues.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLeagues.Location = new Point(26, 12);
            dgvLeagues.MultiSelect = false;
            dgvLeagues.Name = "dgvLeagues";
            dgvLeagues.ReadOnly = true;
            dgvLeagues.RowHeadersVisible = false;
            dgvLeagues.Size = new Size(388, 459);
            dgvLeagues.TabIndex = 0;
            dgvLeagues.CellClick += dgvLeagues_CellClick;
            // 
            // txtName
            // 
            txtName.Font = new Font("Segoe UI", 14.25F);
            txtName.Location = new Point(26, 504);
            txtName.Name = "txtName";
            txtName.Size = new Size(261, 33);
            txtName.TabIndex = 1;
            // 
            // txtSeason
            // 
            txtSeason.Font = new Font("Segoe UI", 14.25F);
            txtSeason.Location = new Point(26, 578);
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
            dgvParticipants.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvParticipants.Location = new Point(466, 12);
            dgvParticipants.MultiSelect = false;
            dgvParticipants.Name = "dgvParticipants";
            dgvParticipants.ReadOnly = true;
            dgvParticipants.RowHeadersVisible = false;
            dgvParticipants.Size = new Size(381, 459);
            dgvParticipants.TabIndex = 0;
            // 
            // cboAvailableClubs
            // 
            cboAvailableClubs.Font = new Font("Segoe UI", 14.25F);
            cboAvailableClubs.FormattingEnabled = true;
            cboAvailableClubs.Location = new Point(536, 549);
            cboAvailableClubs.Name = "cboAvailableClubs";
            cboAvailableClubs.Size = new Size(251, 33);
            cboAvailableClubs.TabIndex = 2;
            // 
            // btnAddClub
            // 
            btnAddClub.Font = new Font("Segoe UI", 14.25F);
            btnAddClub.Location = new Point(506, 627);
            btnAddClub.Name = "btnAddClub";
            btnAddClub.Size = new Size(144, 50);
            btnAddClub.TabIndex = 3;
            btnAddClub.Text = "Добави клуб";
            btnAddClub.UseVisualStyleBackColor = true;
            btnAddClub.Click += btnAddClub_Click;
            // 
            // btnRemoveClub
            // 
            btnRemoveClub.Font = new Font("Segoe UI", 14.25F);
            btnRemoveClub.Location = new Point(688, 627);
            btnRemoveClub.Name = "btnRemoveClub";
            btnRemoveClub.Size = new Size(144, 50);
            btnRemoveClub.TabIndex = 3;
            btnRemoveClub.Text = "Махни клуб";
            btnRemoveClub.UseVisualStyleBackColor = true;
            btnRemoveClub.Click += btnRemoveClub_Click;
            // 
            // btnAddLeague
            // 
            btnAddLeague.BackColor = Color.LightGreen;
            btnAddLeague.Font = new Font("Segoe UI", 14.25F);
            btnAddLeague.Location = new Point(26, 627);
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
            label1.Location = new Point(26, 476);
            label1.Name = "label1";
            label1.Size = new Size(118, 25);
            label1.TabIndex = 5;
            label1.Text = "Име на лига";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F);
            label2.Location = new Point(26, 550);
            label2.Name = "label2";
            label2.Size = new Size(231, 25);
            label2.TabIndex = 5;
            label2.Text = "Сезон (Формат:ГГГГ/ГГГГ)";
            // 
            // btnUpdateLeague
            // 
            btnUpdateLeague.BackColor = Color.LightBlue;
            btnUpdateLeague.Font = new Font("Segoe UI", 14.25F);
            btnUpdateLeague.Location = new Point(223, 627);
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
            btnClear.Location = new Point(223, 693);
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
            label3.Location = new Point(539, 521);
            label3.Name = "label3";
            label3.Size = new Size(248, 25);
            label3.TabIndex = 5;
            label3.Text = "Избор на клуб за добавяне";
            // 
            // btnDeleteLeague
            // 
            btnDeleteLeague.BackColor = Color.Coral;
            btnDeleteLeague.Font = new Font("Segoe UI", 14.25F);
            btnDeleteLeague.Location = new Point(26, 693);
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
            lineSeparator.Location = new Point(441, 24);
            lineSeparator.Name = "lineSeparator";
            lineSeparator.Size = new Size(1, 700);
            lineSeparator.TabIndex = 6;
            // 
            // LeaguesForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(873, 762);
            Controls.Add(lineSeparator);
            Controls.Add(dgvLeagues);
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
            Text = "Лиги";
            Load += LeaguesForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvLeagues).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvParticipants).EndInit();
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
    }
}