namespace Football_Manager
{
    partial class MainForm
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
            btnOpenPlayers = new Button();
            btnOpenClubs = new Button();
            btnOpenTransfers = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // btnOpenPlayers
            // 
            btnOpenPlayers.Font = new Font("Segoe UI", 14.25F);
            btnOpenPlayers.Location = new Point(32, 153);
            btnOpenPlayers.Name = "btnOpenPlayers";
            btnOpenPlayers.Size = new Size(195, 48);
            btnOpenPlayers.TabIndex = 0;
            btnOpenPlayers.Text = "Играчи";
            btnOpenPlayers.UseVisualStyleBackColor = true;
            btnOpenPlayers.Click += btnOpenPlayers_Click;
            // 
            // btnOpenClubs
            // 
            btnOpenClubs.Font = new Font("Segoe UI", 14.25F);
            btnOpenClubs.Location = new Point(32, 85);
            btnOpenClubs.Name = "btnOpenClubs";
            btnOpenClubs.Size = new Size(195, 48);
            btnOpenClubs.TabIndex = 1;
            btnOpenClubs.Text = "Отбори";
            btnOpenClubs.UseVisualStyleBackColor = true;
            btnOpenClubs.Click += btnOpenClubs_Click;
            // 
            // btnOpenTransfers
            // 
            btnOpenTransfers.Font = new Font("Segoe UI", 14.25F);
            btnOpenTransfers.Location = new Point(32, 221);
            btnOpenTransfers.Name = "btnOpenTransfers";
            btnOpenTransfers.Size = new Size(195, 58);
            btnOpenTransfers.TabIndex = 1;
            btnOpenTransfers.Text = "История на трансферите";
            btnOpenTransfers.UseVisualStyleBackColor = true;
            btnOpenTransfers.Click += btnOpenTransfers_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(24, 23);
            label1.Name = "label1";
            label1.Size = new Size(220, 25);
            label1.TabIndex = 2;
            label1.Text = "ФУТБОЛЕН МЕНИДЖЪР";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(276, 328);
            Controls.Add(label1);
            Controls.Add(btnOpenTransfers);
            Controls.Add(btnOpenClubs);
            Controls.Add(btnOpenPlayers);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Футболен мениджър";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnOpenPlayers;
        private Button btnOpenClubs;
        private Button btnOpenTransfers;
        private Label label1;
    }
}