namespace Football_Manager
{
    partial class TransfersHistoryForm
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
            txtSearchNameTransfer = new TextBox();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvTransfers).BeginInit();
            SuspendLayout();
            // 
            // dgvTransfers
            // 
            dgvTransfers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTransfers.Location = new Point(33, 122);
            dgvTransfers.Name = "dgvTransfers";
            dgvTransfers.Size = new Size(738, 316);
            dgvTransfers.TabIndex = 0;
            // 
            // txtSearchNameTransfer
            // 
            txtSearchNameTransfer.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtSearchNameTransfer.Location = new Point(33, 71);
            txtSearchNameTransfer.Name = "txtSearchNameTransfer";
            txtSearchNameTransfer.Size = new Size(254, 33);
            txtSearchNameTransfer.TabIndex = 1;
            txtSearchNameTransfer.TextChanged += txtSearchNameTransfers_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            label1.Location = new Point(261, 9);
            label1.Name = "label1";
            label1.Size = new Size(293, 32);
            label1.TabIndex = 2;
            label1.Text = "История на трансферите";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(33, 43);
            label2.Name = "label2";
            label2.Size = new Size(185, 25);
            label2.TabIndex = 2;
            label2.Text = "Търси играч по име";
            // 
            // TransfersHistoryForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtSearchNameTransfer);
            Controls.Add(dgvTransfers);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "TransfersHistoryForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "История на тансферите";
            Load += TransfersHistoryForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvTransfers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvTransfers;
        private TextBox txtSearchNameTransfer;
        private Label label1;
        private Label label2;
    }
}