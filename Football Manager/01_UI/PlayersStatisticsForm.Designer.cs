namespace Football_Manager.UI
{
    partial class PlayersStatisticsForm
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
            label1 = new Label();
            dgvMostGoals = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvMostGoals).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(71, 31);
            label1.Name = "label1";
            label1.Size = new Size(171, 25);
            label1.TabIndex = 0;
            label1.Text = "Най-много голове";
            // 
            // dgvMostGoals
            // 
            dgvMostGoals.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMostGoals.Location = new Point(12, 74);
            dgvMostGoals.Name = "dgvMostGoals";
            dgvMostGoals.Size = new Size(311, 181);
            dgvMostGoals.TabIndex = 1;
            // 
            // PlayersStatisticsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1345, 568);
            Controls.Add(dgvMostGoals);
            Controls.Add(label1);
            Name = "PlayersStatisticsForm";
            Text = "Статистика";
            ((System.ComponentModel.ISupportInitialize)dgvMostGoals).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private DataGridView dgvMostGoals;
    }
}