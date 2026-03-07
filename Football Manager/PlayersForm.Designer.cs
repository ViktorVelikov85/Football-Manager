namespace Football_Manager
{
    partial class PlayersForm
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
            cboClub = new ComboBox();
            cboPosition = new ComboBox();
            dtpBirthDate = new DateTimePicker();
            cboFilterClub = new ComboBox();
            cboFilterPosition = new ComboBox();
            txtSearchName = new TextBox();
            dgvPlayers = new DataGridView();
            txtFirstName = new TextBox();
            numShirtNumber = new NumericUpDown();
            cboStatus = new ComboBox();
            txtLastName = new TextBox();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvPlayers).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numShirtNumber).BeginInit();
            SuspendLayout();
            // 
            // cboClub
            // 
            cboClub.DropDownStyle = ComboBoxStyle.DropDownList;
            cboClub.Font = new Font("Segoe UI", 14.25F);
            cboClub.FormattingEnabled = true;
            cboClub.Location = new Point(310, 430);
            cboClub.Name = "cboClub";
            cboClub.Size = new Size(200, 33);
            cboClub.TabIndex = 0;
            // 
            // cboPosition
            // 
            cboPosition.DropDownStyle = ComboBoxStyle.DropDownList;
            cboPosition.Font = new Font("Segoe UI", 14.25F);
            cboPosition.FormattingEnabled = true;
            cboPosition.Location = new Point(528, 430);
            cboPosition.Name = "cboPosition";
            cboPosition.Size = new Size(121, 33);
            cboPosition.TabIndex = 1;
            // 
            // dtpBirthDate
            // 
            dtpBirthDate.Font = new Font("Segoe UI", 14.25F);
            dtpBirthDate.Location = new Point(310, 500);
            dtpBirthDate.Name = "dtpBirthDate";
            dtpBirthDate.Size = new Size(339, 33);
            dtpBirthDate.TabIndex = 2;
            // 
            // cboFilterClub
            // 
            cboFilterClub.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFilterClub.Font = new Font("Segoe UI", 14.25F);
            cboFilterClub.FormattingEnabled = true;
            cboFilterClub.Location = new Point(706, 37);
            cboFilterClub.Name = "cboFilterClub";
            cboFilterClub.Size = new Size(142, 33);
            cboFilterClub.TabIndex = 3;
            cboFilterClub.SelectedIndexChanged += cboFilterClub_SelectedIndexChanged;
            // 
            // cboFilterPosition
            // 
            cboFilterPosition.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFilterPosition.Font = new Font("Segoe UI", 14.25F);
            cboFilterPosition.FormattingEnabled = true;
            cboFilterPosition.Location = new Point(872, 38);
            cboFilterPosition.Name = "cboFilterPosition";
            cboFilterPosition.Size = new Size(103, 33);
            cboFilterPosition.TabIndex = 4;
            cboFilterPosition.SelectedIndexChanged += cboFilterPosition_SelectedIndexChanged;
            // 
            // txtSearchName
            // 
            txtSearchName.Font = new Font("Segoe UI", 14.25F);
            txtSearchName.Location = new Point(35, 38);
            txtSearchName.Name = "txtSearchName";
            txtSearchName.Size = new Size(305, 33);
            txtSearchName.TabIndex = 5;
            // 
            // dgvPlayers
            // 
            dgvPlayers.AllowUserToAddRows = false;
            dgvPlayers.AllowUserToDeleteRows = false;
            dgvPlayers.AllowUserToResizeColumns = false;
            dgvPlayers.AllowUserToResizeRows = false;
            dgvPlayers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPlayers.Location = new Point(30, 77);
            dgvPlayers.MultiSelect = false;
            dgvPlayers.Name = "dgvPlayers";
            dgvPlayers.ReadOnly = true;
            dgvPlayers.RowHeadersVisible = false;
            dgvPlayers.Size = new Size(945, 317);
            dgvPlayers.TabIndex = 6;
            dgvPlayers.CellClick += dgvPlayers_CellClick;
            // 
            // txtFirstName
            // 
            txtFirstName.Font = new Font("Segoe UI", 14.25F);
            txtFirstName.Location = new Point(36, 430);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(209, 33);
            txtFirstName.TabIndex = 7;
            // 
            // numShirtNumber
            // 
            numShirtNumber.Font = new Font("Segoe UI", 14.25F);
            numShirtNumber.Location = new Point(36, 573);
            numShirtNumber.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
            numShirtNumber.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numShirtNumber.Name = "numShirtNumber";
            numShirtNumber.Size = new Size(120, 33);
            numShirtNumber.TabIndex = 8;
            numShirtNumber.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // cboStatus
            // 
            cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboStatus.Font = new Font("Segoe UI", 14.25F);
            cboStatus.FormattingEnabled = true;
            cboStatus.Location = new Point(310, 573);
            cboStatus.Name = "cboStatus";
            cboStatus.Size = new Size(160, 33);
            cboStatus.TabIndex = 9;
            // 
            // txtLastName
            // 
            txtLastName.Font = new Font("Segoe UI", 14.25F);
            txtLastName.Location = new Point(36, 500);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(209, 33);
            txtLastName.TabIndex = 10;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.LightGreen;
            btnAdd.Font = new Font("Segoe UI", 14.25F);
            btnAdd.Location = new Point(718, 430);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(120, 60);
            btnAdd.TabIndex = 11;
            btnAdd.Text = "Добави";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.LightBlue;
            btnUpdate.Font = new Font("Segoe UI", 14.25F);
            btnUpdate.Location = new Point(855, 430);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(120, 60);
            btnUpdate.TabIndex = 11;
            btnUpdate.Text = "Обнови";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.Coral;
            btnDelete.Font = new Font("Segoe UI", 14.25F);
            btnDelete.Location = new Point(855, 545);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(120, 60);
            btnDelete.TabIndex = 11;
            btnDelete.Text = "Изтрий";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnClear
            // 
            btnClear.Font = new Font("Segoe UI", 14.25F);
            btnClear.Location = new Point(718, 545);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(120, 60);
            btnClear.TabIndex = 11;
            btnClear.Text = "Изчисти";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F);
            label1.Location = new Point(36, 402);
            label1.Name = "label1";
            label1.Size = new Size(49, 25);
            label1.TabIndex = 13;
            label1.Text = "Име";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F);
            label2.Location = new Point(36, 472);
            label2.Name = "label2";
            label2.Size = new Size(91, 25);
            label2.TabIndex = 13;
            label2.Text = "Фамилия";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14.25F);
            label3.Location = new Point(32, 545);
            label3.Name = "label3";
            label3.Size = new Size(187, 25);
            label3.TabIndex = 13;
            label3.Text = "Номер на тениската";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14.25F);
            label4.Location = new Point(310, 545);
            label4.Name = "label4";
            label4.Size = new Size(68, 25);
            label4.TabIndex = 13;
            label4.Text = "Статус";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 14.25F);
            label5.Location = new Point(310, 472);
            label5.Name = "label5";
            label5.Size = new Size(160, 25);
            label5.TabIndex = 13;
            label5.Text = "Дата на раждане";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 14.25F);
            label6.Location = new Point(310, 402);
            label6.Name = "label6";
            label6.Size = new Size(53, 25);
            label6.TabIndex = 13;
            label6.Text = "Клуб";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 14.25F);
            label7.Location = new Point(528, 402);
            label7.Name = "label7";
            label7.Size = new Size(88, 25);
            label7.TabIndex = 13;
            label7.Text = "Позиция";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.Location = new Point(35, 9);
            label8.Name = "label8";
            label8.Size = new Size(211, 25);
            label8.TabIndex = 14;
            label8.Text = "Търси по име на играч";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 14.25F);
            label9.Location = new Point(604, 9);
            label9.Name = "label9";
            label9.Size = new Size(108, 25);
            label9.TabIndex = 15;
            label9.Text = "Филтър по:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 14.25F);
            label10.Location = new Point(706, 9);
            label10.Name = "label10";
            label10.Size = new Size(51, 25);
            label10.TabIndex = 16;
            label10.Text = "клуб";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 14.25F);
            label11.Location = new Point(872, 9);
            label11.Name = "label11";
            label11.Size = new Size(85, 25);
            label11.TabIndex = 16;
            label11.Text = "позиция";
            // 
            // PlayersForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1003, 626);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnClear);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnAdd);
            Controls.Add(txtLastName);
            Controls.Add(cboStatus);
            Controls.Add(numShirtNumber);
            Controls.Add(txtFirstName);
            Controls.Add(dgvPlayers);
            Controls.Add(txtSearchName);
            Controls.Add(cboFilterPosition);
            Controls.Add(cboFilterClub);
            Controls.Add(dtpBirthDate);
            Controls.Add(cboPosition);
            Controls.Add(cboClub);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MaximizeBox = false;
            Name = "PlayersForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PlayerForm";
            Load += PlayersForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPlayers).EndInit();
            ((System.ComponentModel.ISupportInitialize)numShirtNumber).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cboClub;
        private ComboBox cboPosition;
        private DateTimePicker dtpBirthDate;
        private ComboBox cboFilterClub;
        private ComboBox cboFilterPosition;
        private TextBox txtSearchName;
        private DataGridView dgvPlayers;
        private TextBox txtFirstName;
        private NumericUpDown numShirtNumber;
        private ComboBox cboStatus;
        private TextBox txtLastName;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
    }
}