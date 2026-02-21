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
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            dgvClubs = new DataGridView();
            txtName = new TextBox();
            txtCity = new TextBox();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvClubs).BeginInit();
            SuspendLayout();
            // 
            // dgvClubs
            // 
            dgvClubs.AllowUserToAddRows = false;
            dgvClubs.AllowUserToDeleteRows = false;
            dgvClubs.AllowUserToResizeColumns = false;
            dgvClubs.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dgvClubs.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.Control;
            dataGridViewCellStyle4.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dgvClubs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
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
            txtName.Location = new Point(48, 315);
            txtName.Name = "txtName";
            txtName.Size = new Size(368, 33);
            txtName.TabIndex = 1;
            // 
            // txtCity
            // 
            txtCity.Font = new Font("Segoe UI", 14.25F);
            txtCity.Location = new Point(48, 385);
            txtCity.Name = "txtCity";
            txtCity.Size = new Size(232, 33);
            txtCity.TabIndex = 2;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.LightGreen;
            btnAdd.Font = new Font("Segoe UI", 14.25F);
            btnAdd.Location = new Point(501, 308);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(94, 45);
            btnAdd.TabIndex = 3;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.Font = new Font("Segoe UI", 14.25F);
            btnEdit.Location = new Point(501, 373);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(94, 45);
            btnEdit.TabIndex = 4;
            btnEdit.Text = "Update";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.Coral;
            btnDelete.Font = new Font("Segoe UI", 14.25F);
            btnDelete.Location = new Point(620, 308);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(94, 45);
            btnDelete.TabIndex = 5;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.LightBlue;
            btnClear.Font = new Font("Segoe UI", 14.25F);
            btnClear.Location = new Point(322, 378);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(94, 45);
            btnClear.TabIndex = 6;
            btnClear.Text = "Clear";
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
            // ClubsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(779, 450);
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
            Text = "ClubsForm ";
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
    }
}
