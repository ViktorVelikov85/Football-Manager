namespace Football_Manager
{
    partial class ClubsForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            dgvClubs = new DataGridView();
            txtName = new TextBox();
            txtCity = new TextBox();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            label1 = new Label();
            label2 = new Label();
            txtStadium = new TextBox();
            txtCreatedIn = new TextBox();
            label3 = new Label();
            label4 = new Label();
            btnOpenPlayers = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvClubs).BeginInit();
            SuspendLayout();
            // 
            // dgvClubs
            // 
            dgvClubs.AllowUserToAddRows = false;
            dgvClubs.AllowUserToDeleteRows = false;
            dgvClubs.AllowUserToResizeColumns = false;
            dgvClubs.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dgvClubs.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = SystemColors.Control;
            dataGridViewCellStyle6.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle6.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
            dgvClubs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            dgvClubs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvClubs.Location = new Point(45, 28);
            dgvClubs.MultiSelect = false;
            dgvClubs.Name = "dgvClubs";
            dgvClubs.ReadOnly = true;
            dgvClubs.RowHeadersVisible = false;
            dgvClubs.Size = new Size(693, 242);
            dgvClubs.TabIndex = 0;
            dgvClubs.CellClick += dgvClubs_CellClick;
            // 
            // txtName
            // 
            txtName.Font = new Font("Segoe UI", 14.25F);
            txtName.Location = new Point(45, 315);
            txtName.Name = "txtName";
            txtName.Size = new Size(381, 33);
            txtName.TabIndex = 1;
            // 
            // txtCity
            // 
            txtCity.Font = new Font("Segoe UI", 14.25F);
            txtCity.Location = new Point(45, 385);
            txtCity.Name = "txtCity";
            txtCity.Size = new Size(232, 33);
            txtCity.TabIndex = 2;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.LightGreen;
            btnAdd.Font = new Font("Segoe UI", 14.25F);
            btnAdd.Location = new Point(620, 299);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(120, 60);
            btnAdd.TabIndex = 3;
            btnAdd.Text = "Добави";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.BackColor = Color.LightBlue;
            btnEdit.Font = new Font("Segoe UI", 14.25F);
            btnEdit.Location = new Point(618, 499);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(120, 60);
            btnEdit.TabIndex = 4;
            btnEdit.Text = "Обнови";
            btnEdit.UseVisualStyleBackColor = false;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.Coral;
            btnDelete.Font = new Font("Segoe UI", 14.25F);
            btnDelete.Location = new Point(620, 398);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(120, 60);
            btnDelete.TabIndex = 5;
            btnDelete.Text = "Изтрий";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnClear
            // 
            btnClear.BackColor = SystemColors.Control;
            btnClear.Font = new Font("Segoe UI", 14.25F);
            btnClear.Location = new Point(306, 499);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(120, 60);
            btnClear.TabIndex = 6;
            btnClear.Text = "Изчисти";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F);
            label1.Location = new Point(45, 287);
            label1.Name = "label1";
            label1.Size = new Size(132, 25);
            label1.TabIndex = 7;
            label1.Text = "Име на отбор";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F);
            label2.Location = new Point(45, 357);
            label2.Name = "label2";
            label2.Size = new Size(52, 25);
            label2.TabIndex = 8;
            label2.Text = "Град";
            // 
            // txtStadium
            // 
            txtStadium.Font = new Font("Segoe UI", 14.25F);
            txtStadium.Location = new Point(45, 457);
            txtStadium.Name = "txtStadium";
            txtStadium.Size = new Size(230, 33);
            txtStadium.TabIndex = 9;
            // 
            // txtCreatedIn
            // 
            txtCreatedIn.Font = new Font("Segoe UI", 14.25F);
            txtCreatedIn.Location = new Point(45, 527);
            txtCreatedIn.Name = "txtCreatedIn";
            txtCreatedIn.Size = new Size(100, 33);
            txtCreatedIn.TabIndex = 10;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14.25F);
            label3.Location = new Point(45, 429);
            label3.Name = "label3";
            label3.Size = new Size(85, 25);
            label3.TabIndex = 11;
            label3.Text = "Стадион";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14.25F);
            label4.Location = new Point(45, 499);
            label4.Name = "label4";
            label4.Size = new Size(194, 25);
            label4.TabIndex = 12;
            label4.Text = "Година на създаване";
            // 
            // btnOpenPlayers
            // 
            btnOpenPlayers.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnOpenPlayers.Location = new Point(483, 300);
            btnOpenPlayers.Name = "btnOpenPlayers";
            btnOpenPlayers.Size = new Size(120, 60);
            btnOpenPlayers.TabIndex = 13;
            btnOpenPlayers.Text = "Играчи";
            btnOpenPlayers.UseVisualStyleBackColor = true;
            btnOpenPlayers.Click += btnOpenPlayers_Click;
            // 
            // ClubsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(781, 585);
            Controls.Add(btnOpenPlayers);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(txtCreatedIn);
            Controls.Add(txtStadium);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnClear);
            Controls.Add(btnDelete);
            Controls.Add(btnEdit);
            Controls.Add(btnAdd);
            Controls.Add(txtCity);
            Controls.Add(txtName);
            Controls.Add(dgvClubs);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "ClubsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Отбори";
            FormClosed += ClubsForm_FormClosed;
            Shown += ClubsForm_Shown;
            ((System.ComponentModel.ISupportInitialize)dgvClubs).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvClubs;
        private TextBox txtName;
        private TextBox txtCity;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnClear;
        private Label label1;
        private Label label2;
        private TextBox txtStadium;
        private TextBox txtCreatedIn;
        private Label label3;
        private Label label4;
        private Button btnOpenPlayers;
    }
}
