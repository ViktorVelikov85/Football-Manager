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
            panelPodium = new Panel();
            label2 = new Label();
            panelMostCards = new Panel();
            panelMostFauls = new Panel();
            label3 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(192, 9);
            label1.Name = "label1";
            label1.Size = new Size(171, 25);
            label1.TabIndex = 0;
            label1.Text = "Най-много голове";
            // 
            // panelPodium
            // 
            panelPodium.Location = new Point(12, 58);
            panelPodium.Name = "panelPodium";
            panelPodium.Size = new Size(485, 284);
            panelPodium.TabIndex = 1;
            panelPodium.Paint += panelPodium_Paint;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(735, 9);
            label2.Name = "label2";
            label2.Size = new Size(183, 25);
            label2.TabIndex = 0;
            label2.Text = "Най-много картони";
            // 
            // panelMostCards
            // 
            panelMostCards.Location = new Point(551, 58);
            panelMostCards.Name = "panelMostCards";
            panelMostCards.Size = new Size(547, 434);
            panelMostCards.TabIndex = 2;
            panelMostCards.Paint += panelMostCards_Paint;
            // 
            // panelMostFauls
            // 
            panelMostFauls.Location = new Point(1154, 58);
            panelMostFauls.Name = "panelMostFauls";
            panelMostFauls.Size = new Size(430, 434);
            panelMostFauls.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(1292, 9);
            label3.Name = "label3";
            label3.Size = new Size(176, 25);
            label3.TabIndex = 0;
            label3.Text = "Най-много фалове";
            // 
            // PlayersStatisticsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1596, 568);
            Controls.Add(panelMostFauls);
            Controls.Add(panelMostCards);
            Controls.Add(panelPodium);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "PlayersStatisticsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Статистика";
            Load += PlayersStatisticsForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Panel panelPodium;
        private Label label2;
        private Panel panel1;
        private Panel panelMostFauls;
        private Label label3;
        private Panel panelMostCards;
    }
}