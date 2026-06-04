namespace Football_Manager.UI
{
    partial class MatchManagementForm
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
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            dgvEvents = new DataGridView();
            colMinute = new DataGridViewTextBoxColumn();
            colEventType = new DataGridViewTextBoxColumn();
            colPlayerName = new DataGridViewTextBoxColumn();
            colClubName = new DataGridViewTextBoxColumn();
            lblResult = new Label();
            cboEventType = new ComboBox();
            cboPlayers = new ComboBox();
            nudMinute = new NumericUpDown();
            btnAddEvent = new Button();
            btnRemoveEvent = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            lblHomeTeam = new Label();
            lblVs = new Label();
            lblAwayTeam = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvEvents).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudMinute).BeginInit();
            SuspendLayout();
            // 
            // dgvEvents
            // 
            dgvEvents.AllowUserToAddRows = false;
            dgvEvents.AllowUserToDeleteRows = false;
            dgvEvents.AllowUserToResizeColumns = false;
            dgvEvents.AllowUserToResizeRows = false;
            dgvEvents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvEvents.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvEvents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEvents.Columns.AddRange(new DataGridViewColumn[] { colMinute, colEventType, colPlayerName, colClubName });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvEvents.DefaultCellStyle = dataGridViewCellStyle2;
            dgvEvents.Location = new Point(12, 84);
            dgvEvents.MultiSelect = false;
            dgvEvents.Name = "dgvEvents";
            dgvEvents.ReadOnly = true;
            dgvEvents.RowHeadersVisible = false;
            dgvEvents.RowTemplate.Height = 30;
            dgvEvents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEvents.Size = new Size(723, 363);
            dgvEvents.TabIndex = 2;
            // 
            // colMinute
            // 
            colMinute.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colMinute.DataPropertyName = "minute";
            colMinute.HeaderText = "Мин";
            colMinute.Name = "colMinute";
            colMinute.ReadOnly = true;
            colMinute.Width = 50;
            // 
            // colEventType
            // 
            colEventType.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colEventType.DataPropertyName = "event_type";
            colEventType.HeaderText = "Събитие";
            colEventType.Name = "colEventType";
            colEventType.ReadOnly = true;
            // 
            // colPlayerName
            // 
            colPlayerName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colPlayerName.DataPropertyName = "player_name";
            colPlayerName.HeaderText = "Играч";
            colPlayerName.Name = "colPlayerName";
            colPlayerName.ReadOnly = true;
            // 
            // colClubName
            // 
            colClubName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colClubName.DataPropertyName = "club_name";
            colClubName.HeaderText = "Отбор";
            colClubName.Name = "colClubName";
            colClubName.ReadOnly = true;
            // 
            // lblResult
            // 
            lblResult.Anchor = AnchorStyles.Top;
            lblResult.Font = new Font("Arial", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblResult.Location = new Point(306, 52);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(124, 29);
            lblResult.TabIndex = 3;
            lblResult.Text = "0 - 0";
            lblResult.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cboEventType
            // 
            cboEventType.Font = new Font("Arial", 14.25F);
            cboEventType.FormattingEnabled = true;
            cboEventType.Items.AddRange(new object[] { "Гол", "Жълт картон", "Червен картон", "Фаул" });
            cboEventType.Location = new Point(207, 512);
            cboEventType.Name = "cboEventType";
            cboEventType.Size = new Size(121, 30);
            cboEventType.TabIndex = 4;
            // 
            // cboPlayers
            // 
            cboPlayers.Font = new Font("Arial", 14.25F);
            cboPlayers.FormattingEnabled = true;
            cboPlayers.Location = new Point(404, 511);
            cboPlayers.Name = "cboPlayers";
            cboPlayers.Size = new Size(321, 30);
            cboPlayers.TabIndex = 4;
            // 
            // nudMinute
            // 
            nudMinute.Font = new Font("Arial", 14.25F);
            nudMinute.Location = new Point(74, 512);
            nudMinute.Maximum = new decimal(new int[] { 120, 0, 0, 0 });
            nudMinute.Name = "nudMinute";
            nudMinute.Size = new Size(74, 29);
            nudMinute.TabIndex = 5;
            // 
            // btnAddEvent
            // 
            btnAddEvent.BackColor = Color.LightGreen;
            btnAddEvent.Font = new Font("Arial", 14.25F);
            btnAddEvent.Location = new Point(184, 576);
            btnAddEvent.Name = "btnAddEvent";
            btnAddEvent.Size = new Size(144, 64);
            btnAddEvent.TabIndex = 6;
            btnAddEvent.Text = "Добави събитие";
            btnAddEvent.UseVisualStyleBackColor = false;
            btnAddEvent.Click += btnAddEvent_Click;
            // 
            // btnRemoveEvent
            // 
            btnRemoveEvent.BackColor = Color.Coral;
            btnRemoveEvent.Font = new Font("Arial", 14.25F);
            btnRemoveEvent.Location = new Point(404, 576);
            btnRemoveEvent.Name = "btnRemoveEvent";
            btnRemoveEvent.Size = new Size(144, 64);
            btnRemoveEvent.TabIndex = 6;
            btnRemoveEvent.Text = "Изтрий събитие";
            btnRemoveEvent.UseVisualStyleBackColor = false;
            btnRemoveEvent.Click += btnRemoveEvent_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial", 14.25F);
            label1.Location = new Point(22, 486);
            label1.Name = "label1";
            label1.Size = new Size(174, 22);
            label1.TabIndex = 3;
            label1.Text = "Минута на събитие";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Arial", 14.25F);
            label2.Location = new Point(204, 484);
            label2.Name = "label2";
            label2.Size = new Size(121, 22);
            label2.TabIndex = 3;
            label2.Text = "Вид събитие";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Arial", 14.25F);
            label3.Location = new Point(404, 486);
            label3.Name = "label3";
            label3.Size = new Size(62, 22);
            label3.TabIndex = 3;
            label3.Text = "Играч";
            // 
            // lblHomeTeam
            // 
            lblHomeTeam.Font = new Font("Arial", 14.25F, FontStyle.Bold);
            lblHomeTeam.Location = new Point(12, 23);
            lblHomeTeam.Name = "lblHomeTeam";
            lblHomeTeam.Size = new Size(330, 30);
            lblHomeTeam.TabIndex = 3;
            lblHomeTeam.Text = "Домакин";
            lblHomeTeam.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblVs
            // 
            lblVs.Anchor = AnchorStyles.Top;
            lblVs.Font = new Font("Arial", 14.25F, FontStyle.Bold);
            lblVs.Location = new Point(348, 23);
            lblVs.Name = "lblVs";
            lblVs.Size = new Size(39, 30);
            lblVs.TabIndex = 3;
            lblVs.Text = "VS";
            lblVs.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblAwayTeam
            // 
            lblAwayTeam.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblAwayTeam.Font = new Font("Arial", 14.25F, FontStyle.Bold);
            lblAwayTeam.Location = new Point(393, 23);
            lblAwayTeam.Name = "lblAwayTeam";
            lblAwayTeam.Size = new Size(341, 30);
            lblAwayTeam.TabIndex = 3;
            lblAwayTeam.Text = "Гост";
            lblAwayTeam.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // MatchManagementForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(745, 681);
            Controls.Add(btnRemoveEvent);
            Controls.Add(btnAddEvent);
            Controls.Add(nudMinute);
            Controls.Add(cboPlayers);
            Controls.Add(cboEventType);
            Controls.Add(lblAwayTeam);
            Controls.Add(lblVs);
            Controls.Add(lblHomeTeam);
            Controls.Add(lblResult);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dgvEvents);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "MatchManagementForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MatchesForm";
            Load += MatchManagementForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvEvents).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudMinute).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DataGridView dgvEvents;
        private Label lblResult;
        private ComboBox cboEventType;
        private ComboBox cboPlayers;
        private NumericUpDown nudMinute;
        private Button btnAddEvent;
        private Button btnRemoveEvent;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label lblHomeTeam;
        private Label lblVs;
        private Label lblAwayTeam;
        private DataGridViewTextBoxColumn colMinute;
        private DataGridViewTextBoxColumn colEventType;
        private DataGridViewTextBoxColumn colPlayerName;
        private DataGridViewTextBoxColumn colClubName;
    }
}