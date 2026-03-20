namespace Football_Manager
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
            dgvTransfers = new DataGridView();
            cboPlayer = new ComboBox();
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
            ((System.ComponentModel.ISupportInitialize)dgvTransfers).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numFee).BeginInit();
            SuspendLayout();
            // 
            // dgvTransfers
            // 
            dgvTransfers.AllowUserToAddRows = false;
            dgvTransfers.AllowUserToDeleteRows = false;
            dgvTransfers.AllowUserToResizeColumns = false;
            dgvTransfers.AllowUserToResizeRows = false;
            dgvTransfers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTransfers.Location = new Point(32, 22);
            dgvTransfers.MultiSelect = false;
            dgvTransfers.Name = "dgvTransfers";
            dgvTransfers.ReadOnly = true;
            dgvTransfers.RowHeadersVisible = false;
            dgvTransfers.Size = new Size(735, 239);
            dgvTransfers.TabIndex = 0;
            // 
            // cboPlayer
            // 
            cboPlayer.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboPlayer.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboPlayer.Font = new Font("Segoe UI", 14.25F);
            cboPlayer.FormattingEnabled = true;
            cboPlayer.Location = new Point(32, 297);
            cboPlayer.Name = "cboPlayer";
            cboPlayer.Size = new Size(274, 33);
            cboPlayer.TabIndex = 1;
            cboPlayer.SelectedIndexChanged += cboPlayer_SelectedIndexChanged;
            // 
            // cboToClub
            // 
            cboToClub.DropDownStyle = ComboBoxStyle.DropDownList;
            cboToClub.Font = new Font("Segoe UI", 14.25F);
            cboToClub.FormattingEnabled = true;
            cboToClub.Location = new Point(328, 365);
            cboToClub.Name = "cboToClub";
            cboToClub.Size = new Size(259, 33);
            cboToClub.TabIndex = 2;
            // 
            // dtpTransferDate
            // 
            dtpTransferDate.Font = new Font("Segoe UI", 14.25F);
            dtpTransferDate.Location = new Point(32, 453);
            dtpTransferDate.Name = "dtpTransferDate";
            dtpTransferDate.Size = new Size(316, 33);
            dtpTransferDate.TabIndex = 3;
            // 
            // numFee
            // 
            numFee.Font = new Font("Segoe UI", 14.25F);
            numFee.Location = new Point(328, 297);
            numFee.Name = "numFee";
            numFee.Size = new Size(120, 33);
            numFee.TabIndex = 4;
            // 
            // btnTransfer
            // 
            btnTransfer.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnTransfer.Location = new Point(627, 450);
            btnTransfer.Name = "btnTransfer";
            btnTransfer.Size = new Size(119, 43);
            btnTransfer.TabIndex = 5;
            btnTransfer.Text = "Трансфер";
            btnTransfer.UseVisualStyleBackColor = true;
            btnTransfer.Click += btnTransfer_Click;
            // 
            // txtFromClub
            // 
            txtFromClub.Font = new Font("Segoe UI", 14.25F);
            txtFromClub.Location = new Point(32, 365);
            txtFromClub.Name = "txtFromClub";
            txtFromClub.ReadOnly = true;
            txtFromClub.Size = new Size(274, 33);
            txtFromClub.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F);
            label1.Location = new Point(32, 264);
            label1.Name = "label1";
            label1.Size = new Size(130, 25);
            label1.TabIndex = 7;
            label1.Text = "Име на играч";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F);
            label2.Location = new Point(32, 337);
            label2.Name = "label2";
            label2.Size = new Size(78, 25);
            label2.TabIndex = 7;
            label2.Text = "От клуб";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14.25F);
            label3.Location = new Point(328, 337);
            label3.Name = "label3";
            label3.Size = new Size(91, 25);
            label3.TabIndex = 7;
            label3.Text = "Към клуб";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14.25F);
            label4.Location = new Point(328, 269);
            label4.Name = "label4";
            label4.Size = new Size(60, 25);
            label4.TabIndex = 7;
            label4.Text = "Такса";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 14.25F);
            label5.Location = new Point(32, 419);
            label5.Name = "label5";
            label5.Size = new Size(167, 25);
            label5.TabIndex = 7;
            label5.Text = "Дата на трансфер";
            // 
            // btnClear
            // 
            btnClear.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnClear.Location = new Point(493, 450);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(119, 43);
            btnClear.TabIndex = 5;
            btnClear.Text = "Изчисти";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // TransfersForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(789, 517);
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
            Controls.Add(cboPlayer);
            Controls.Add(dgvTransfers);
            Name = "TransfersForm";
            Text = "Трансфери";
            Load += TransfersForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvTransfers).EndInit();
            ((System.ComponentModel.ISupportInitialize)numFee).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvTransfers;
        private ComboBox cboPlayer;
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
    }
}