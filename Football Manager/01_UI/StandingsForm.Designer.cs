namespace Football_Manager.UI
{
    partial class StandingsForm
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
            lblSelectLeague = new Label();
            cboLeagues = new ComboBox();
            btnRefresh = new Button();
            dgvStandings = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvStandings).BeginInit();
            SuspendLayout();
            // 
            // lblSelectLeague
            // 
            lblSelectLeague.AutoSize = true;
            lblSelectLeague.Font = new Font("Segoe UI", 14.25F);
            lblSelectLeague.Location = new Point(176, 333);
            lblSelectLeague.Name = "lblSelectLeague";
            lblSelectLeague.Size = new Size(141, 25);
            lblSelectLeague.TabIndex = 0;
            lblSelectLeague.Text = "Изберете лига:";
            // 
            // cboLeagues
            // 
            cboLeagues.Font = new Font("Segoe UI", 14.25F);
            cboLeagues.FormattingEnabled = true;
            cboLeagues.Location = new Point(323, 330);
            cboLeagues.Name = "cboLeagues";
            cboLeagues.Size = new Size(299, 33);
            cboLeagues.TabIndex = 1;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.LightGreen;
            btnRefresh.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRefresh.Location = new Point(323, 391);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(145, 47);
            btnRefresh.TabIndex = 2;
            btnRefresh.Text = "Презареди";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // dgvStandings
            // 
            dgvStandings.AllowUserToAddRows = false;
            dgvStandings.AllowUserToDeleteRows = false;
            dgvStandings.AllowUserToResizeColumns = false;
            dgvStandings.AllowUserToResizeRows = false;
            dgvStandings.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStandings.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Window;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dgvStandings.DefaultCellStyle = dataGridViewCellStyle1;
            dgvStandings.Location = new Point(12, 12);
            dgvStandings.MultiSelect = false;
            dgvStandings.Name = "dgvStandings";
            dgvStandings.ReadOnly = true;
            dgvStandings.RowHeadersVisible = false;
            dgvStandings.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStandings.Size = new Size(776, 289);
            dgvStandings.TabIndex = 3;
            // 
            // StandingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvStandings);
            Controls.Add(btnRefresh);
            Controls.Add(cboLeagues);
            Controls.Add(lblSelectLeague);
            Name = "StandingsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "StandingsForm";
            Load += StandingsForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvStandings).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblSelectLeague;
        private ComboBox cboLeagues;
        private Button btnRefresh;
        private DataGridView dgvStandings;
    }
}