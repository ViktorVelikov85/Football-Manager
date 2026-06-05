using System;
using System.Drawing;
using System.Windows.Forms;
namespace Football_Manager.UI
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            dgvTransfers = new DataGridView();
            colPlayerName = new DataGridViewTextBoxColumn();
            colOldClub = new DataGridViewTextBoxColumn();
            colNewClub = new DataGridViewTextBoxColumn();
            colAmount = new DataGridViewTextBoxColumn();
            colDate = new DataGridViewTextBoxColumn();
            txtSearchNameTransfer = new TextBox();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvTransfers).BeginInit();
            SuspendLayout();
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
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
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
            dgvTransfers.Location = new Point(12, 122);
            dgvTransfers.MultiSelect = false;
            dgvTransfers.Name = "dgvTransfers";
            dgvTransfers.ReadOnly = true;
            dgvTransfers.RowHeadersVisible = false;
            dgvTransfers.RowTemplate.Height = 30;
            dgvTransfers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTransfers.Size = new Size(804, 352);
            dgvTransfers.TabIndex = 0;
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
            // txtSearchNameTransfer
            // 
            txtSearchNameTransfer.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtSearchNameTransfer.Location = new Point(12, 70);
            txtSearchNameTransfer.Name = "txtSearchNameTransfer";
            txtSearchNameTransfer.Size = new Size(254, 33);
            txtSearchNameTransfer.TabIndex = 1;
            txtSearchNameTransfer.TextChanged += txtSearchNameTransfer_TextChanged;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(804, 32);
            label1.TabIndex = 2;
            label1.Text = "История на трансферите";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(12, 42);
            label2.Name = "label2";
            label2.Size = new Size(185, 25);
            label2.TabIndex = 2;
            label2.Text = "Търси играч по име";
            // 
            // TransfersHistoryForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(828, 486);
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
        private DataGridViewTextBoxColumn colPlayerName;
        private DataGridViewTextBoxColumn colOldClub;
        private DataGridViewTextBoxColumn colNewClub;
        private DataGridViewTextBoxColumn colAmount;
        private DataGridViewTextBoxColumn colDate;
    }
}