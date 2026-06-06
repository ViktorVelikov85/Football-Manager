using System;
using System.Drawing;
using System.Windows.Forms;
namespace Football_Manager.UI
{
    partial class TransfersForm
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
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            cboToClub = new ComboBox();
            dtpTransferDate = new DateTimePicker();
            numFee = new NumericUpDown();
            btnTransfer = new Button();
            txtFromClub = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            btnClear = new Button();
            txtSearchNameTransfer = new TextBox();
            label6 = new Label();
            txtPlayer = new TextBox();
            label7 = new Label();
            dgvTransfers = new DataGridView();
            colPlayerName = new DataGridViewTextBoxColumn();
            colOldClub = new DataGridViewTextBoxColumn();
            colNewClub = new DataGridViewTextBoxColumn();
            colAmount = new DataGridViewTextBoxColumn();
            colDate = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)numFee).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvTransfers).BeginInit();
            SuspendLayout();
            // 
            // cboToClub
            // 
            cboToClub.DropDownStyle = ComboBoxStyle.DropDownList;
            cboToClub.Font = new Font("Segoe UI", 14.25F);
            cboToClub.FormattingEnabled = true;
            cboToClub.Location = new Point(312, 437);
            cboToClub.Name = "cboToClub";
            cboToClub.Size = new Size(259, 33);
            cboToClub.TabIndex = 2;
            // 
            // dtpTransferDate
            // 
            dtpTransferDate.CustomFormat = "dd MMMM yyyy";
            dtpTransferDate.Font = new Font("Segoe UI", 14.25F);
            dtpTransferDate.Format = DateTimePickerFormat.Custom;
            dtpTransferDate.Location = new Point(12, 505);
            dtpTransferDate.Name = "dtpTransferDate";
            dtpTransferDate.Size = new Size(219, 33);
            dtpTransferDate.TabIndex = 3;
            // 
            // numFee
            // 
            numFee.DecimalPlaces = 2;
            numFee.Font = new Font("Segoe UI", 14.25F);
            numFee.Location = new Point(312, 369);
            numFee.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            numFee.Name = "numFee";
            numFee.Size = new Size(172, 33);
            numFee.TabIndex = 4;
            numFee.ThousandsSeparator = true;
            // 
            // btnTransfer
            // 
            btnTransfer.BackColor = Color.LightGreen;
            btnTransfer.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnTransfer.Location = new Point(734, 409);
            btnTransfer.Name = "btnTransfer";
            btnTransfer.Size = new Size(120, 60);
            btnTransfer.TabIndex = 5;
            btnTransfer.Text = "Трансфер";
            btnTransfer.UseVisualStyleBackColor = false;
            btnTransfer.Click += btnTransfer_Click;
            // 
            // txtFromClub
            // 
            txtFromClub.Font = new Font("Segoe UI", 14.25F);
            txtFromClub.Location = new Point(12, 437);
            txtFromClub.Name = "txtFromClub";
            txtFromClub.ReadOnly = true;
            txtFromClub.Size = new Size(274, 33);
            txtFromClub.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F);
            label1.Location = new Point(12, 341);
            label1.Name = "label1";
            label1.Size = new Size(130, 25);
            label1.TabIndex = 7;
            label1.Text = "Име на играч";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F);
            label2.Location = new Point(12, 409);
            label2.Name = "label2";
            label2.Size = new Size(91, 25);
            label2.TabIndex = 7;
            label2.Text = "От отбор";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14.25F);
            label3.Location = new Point(312, 409);
            label3.Name = "label3";
            label3.Size = new Size(104, 25);
            label3.TabIndex = 7;
            label3.Text = "Към отбор";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14.25F);
            label4.Location = new Point(312, 341);
            label4.Name = "label4";
            label4.Size = new Size(75, 25);
            label4.TabIndex = 7;
            label4.Text = "Такса €";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 14.25F);
            label5.Location = new Point(12, 477);
            label5.Name = "label5";
            label5.Size = new Size(167, 25);
            label5.TabIndex = 7;
            label5.Text = "Дата на трансфер";
            // 
            // btnClear
            // 
            btnClear.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnClear.Location = new Point(734, 493);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(120, 60);
            btnClear.TabIndex = 5;
            btnClear.Text = "Изчисти";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // txtSearchNameTransfer
            // 
            txtSearchNameTransfer.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtSearchNameTransfer.Location = new Point(31, 37);
            txtSearchNameTransfer.Name = "txtSearchNameTransfer";
            txtSearchNameTransfer.Size = new Size(272, 33);
            txtSearchNameTransfer.TabIndex = 8;
            txtSearchNameTransfer.TextChanged += txtSearchNameTransfer_TextChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(31, 9);
            label6.Name = "label6";
            label6.Size = new Size(211, 25);
            label6.TabIndex = 9;
            label6.Text = "Търси по име на играч";
            // 
            // txtPlayer
            // 
            txtPlayer.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPlayer.Location = new Point(12, 368);
            txtPlayer.Name = "txtPlayer";
            txtPlayer.ReadOnly = true;
            txtPlayer.Size = new Size(272, 33);
            txtPlayer.TabIndex = 10;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(326, 62);
            label7.Name = "label7";
            label7.Size = new Size(204, 25);
            label7.TabIndex = 11;
            label7.Text = "Предишни трансфери";
            // 
            // dgvTransfers
            // 
            dgvTransfers.AllowUserToAddRows = false;
            dgvTransfers.AllowUserToDeleteRows = false;
            dgvTransfers.AllowUserToResizeColumns = false;
            dgvTransfers.AllowUserToResizeRows = false;
            dgvTransfers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvTransfers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvTransfers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTransfers.Columns.AddRange(new DataGridViewColumn[] { colPlayerName, colOldClub, colNewClub, colAmount, colDate });
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.Window;
            dataGridViewCellStyle4.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle4.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Window;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            dgvTransfers.DefaultCellStyle = dataGridViewCellStyle4;
            dgvTransfers.Location = new Point(12, 90);
            dgvTransfers.MultiSelect = false;
            dgvTransfers.Name = "dgvTransfers";
            dgvTransfers.ReadOnly = true;
            dgvTransfers.RowHeadersVisible = false;
            dgvTransfers.RowTemplate.Height = 30;
            dgvTransfers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTransfers.Size = new Size(842, 248);
            dgvTransfers.TabIndex = 12;
            // 
            // colPlayerName
            // 
            colPlayerName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colPlayerName.DataPropertyName = "player_name";
            colPlayerName.HeaderText = "Играч";
            colPlayerName.Name = "colPlayerName";
            colPlayerName.ReadOnly = true;
            // 
            // colOldClub
            // 
            colOldClub.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colOldClub.DataPropertyName = "old_club_name";
            colOldClub.HeaderText = "Стар отбор";
            colOldClub.Name = "colOldClub";
            colOldClub.ReadOnly = true;
            // 
            // colNewClub
            // 
            colNewClub.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colNewClub.DataPropertyName = "new_club_name";
            colNewClub.HeaderText = "Нов отбор";
            colNewClub.Name = "colNewClub";
            colNewClub.ReadOnly = true;
            // 
            // colAmount
            // 
            colAmount.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colAmount.DataPropertyName = "transfer_fee";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "#,##0\" €\"";
            colAmount.DefaultCellStyle = dataGridViewCellStyle2;
            colAmount.HeaderText = "Сума (€)";
            colAmount.Name = "colAmount";
            colAmount.ReadOnly = true;
            colAmount.Width = 120;
            // 
            // colDate
            // 
            colDate.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colDate.DataPropertyName = "transfer_date";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Format = "dd.MM.yyyy";
            colDate.DefaultCellStyle = dataGridViewCellStyle3;
            colDate.HeaderText = "Дата";
            colDate.Name = "colDate";
            colDate.ReadOnly = true;
            colDate.Width = 120;
            // 
            // TransfersForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(866, 568);
            Controls.Add(dgvTransfers);
            Controls.Add(label7);
            Controls.Add(txtPlayer);
            Controls.Add(label6);
            Controls.Add(txtSearchNameTransfer);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtFromClub);
            Controls.Add(btnClear);
            Controls.Add(btnTransfer);
            Controls.Add(numFee);
            Controls.Add(dtpTransferDate);
            Controls.Add(cboToClub);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "TransfersForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Трансфер на играч";
            Load += TransfersForm_Load;
            ((System.ComponentModel.ISupportInitialize)numFee).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvTransfers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ComboBox cboToClub;
        private DateTimePicker dtpTransferDate;
        private NumericUpDown numFee;
        private Button btnTransfer;
        private TextBox txtFromClub;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Button btnClear;
        private TextBox txtSearchNameTransfer;
        private Label label6;
        private TextBox txtPlayer;
        private Label label7;
        private DataGridView dgvTransfers;
        private DataGridViewTextBoxColumn colPlayerName;
        private DataGridViewTextBoxColumn colOldClub;
        private DataGridViewTextBoxColumn colNewClub;
        private DataGridViewTextBoxColumn colAmount;
        private DataGridViewTextBoxColumn colDate;
    }
}