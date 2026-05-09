using Football_Manager.BLL;
using Football_Manager.Models;
using System.Data;

namespace Football_Manager.UI
{
    public partial class LeaguesForm : Form
    {
        private readonly LeaguesService _leagueService = new LeaguesService();
        private int _selectedLeagueId = -1;

        public LeaguesForm()
        {
            InitializeComponent();
        }

        private void LeaguesForm_Load(object sender, EventArgs e)
        {
            SetGlobalStyles();
            LoadLeagues();
        }

        // --- ФОРМАТИРАНЕ И СТИЛ ---
        private void SetGlobalStyles()
        {
            Font arial12 = new Font("Arial", 12);
            Font arial12Bold = new Font("Arial", 12, FontStyle.Bold);

            DataGridView[] grids = { dgvLeagues, dgvParticipants, dgvSchedule };

            foreach (var g in grids)
            {
                if (g == null) continue;
                g.DefaultCellStyle.Font = arial12;
                g.ColumnHeadersDefaultCellStyle.Font = arial12Bold;
                g.RowTemplate.Height = 35;
            }
        }

        // --- УПРАВЛЕНИЕ НА ЛИГИ (CRUD) ---
        private void LoadLeagues()
        {
            try
            {
                DataTable dt = _leagueService.GetLeagues();
                dgvLeagues.DataSource = dt;

                if (dgvLeagues.Columns.Contains("id")) dgvLeagues.Columns["id"].Visible = false;
                if (dgvLeagues.Columns.Contains("name")) dgvLeagues.Columns["name"].HeaderText = "Лига";
                if (dgvLeagues.Columns.Contains("season")) dgvLeagues.Columns["season"].HeaderText = "Сезон";

                if (dgvLeagues.Rows.Count > 0) SelectLeague(0);
                else ClearInputs();
            }
            catch (Exception ex) { MessageBox.Show("Грешка: " + ex.Message); }
        }

        private void SelectLeague(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dgvLeagues.Rows.Count) return;

            var row = dgvLeagues.Rows[rowIndex];
            _selectedLeagueId = Convert.ToInt32(row.Cells["id"].Value);

            txtName.Text = row.Cells["name"].Value?.ToString();
            txtSeason.Text = row.Cells["season"].Value?.ToString();

            RefreshParticipants();
            RefreshSchedule();
        }

        private void dgvLeagues_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) SelectLeague(e.RowIndex);
        }

        private void btnAddLeague_Click(object sender, EventArgs e)
        {
            var league = new League { Name = txtName.Text.Trim(), Season = txtSeason.Text.Trim() };
            if (_leagueService.SaveLeague(league, true, out string msg))
            {
                MessageBox.Show(msg);
                LoadLeagues();
            }
            else MessageBox.Show(msg, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnUpdateLeague_Click(object sender, EventArgs e)
        {
            if (_selectedLeagueId == -1) return;
            var league = new League { Id = _selectedLeagueId, Name = txtName.Text.Trim(), Season = txtSeason.Text.Trim() };
            if (_leagueService.SaveLeague(league, false, out string msg))
            {
                MessageBox.Show(msg);
                LoadLeagues();
            }
            else MessageBox.Show(msg, "Грешка");
        }

        private void btnDeleteLeague_Click(object sender, EventArgs e)
        {
            if (_selectedLeagueId == -1) return;
            if (MessageBox.Show("Изтриване на лигата и всички мачове?", "Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _leagueService.DeleteLeague(_selectedLeagueId);
                _selectedLeagueId = -1;
                LoadLeagues();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void ClearInputs()
        {
            _selectedLeagueId = -1;
            txtName.Clear();
            txtSeason.Clear();
            dgvParticipants.DataSource = null;
            dgvSchedule.DataSource = null;
            txtName.Focus();
        }

        // --- УПРАВЛЕНИЕ НА УЧАСТНИЦИ ---
        private void RefreshParticipants()
        {
            if (_selectedLeagueId == -1) return;
            dgvParticipants.DataSource = _leagueService.GetParticipants(_selectedLeagueId);

            if (dgvParticipants.Columns.Contains("id")) dgvParticipants.Columns["id"].Visible = false;
            if (dgvParticipants.Columns.Contains("name")) dgvParticipants.Columns["name"].HeaderText = "Отбор";
            if (dgvParticipants.Columns.Contains("city")) dgvParticipants.Columns["city"].HeaderText = "Град";

            DataTable available = _leagueService.GetAvailableClubs(_selectedLeagueId);
            cboAvailableClubs.DataSource = available;
            cboAvailableClubs.DisplayMember = "name";
            cboAvailableClubs.ValueMember = "id";
        }

        private void btnAddClub_Click(object sender, EventArgs e)
        {
            if (_selectedLeagueId == -1 || cboAvailableClubs.SelectedValue == null) return;
            _leagueService.AddClubToLeague(_selectedLeagueId, Convert.ToInt32(cboAvailableClubs.SelectedValue));
            RefreshParticipants();
        }

        private void btnRemoveClub_Click(object sender, EventArgs e)
        {
            if (_selectedLeagueId == -1 || dgvParticipants.CurrentRow == null) return;
            int clubId = Convert.ToInt32(dgvParticipants.CurrentRow.Cells["id"].Value);
            _leagueService.RemoveClubFromLeague(_selectedLeagueId, clubId);
            RefreshParticipants();
        }

        // --- ПРОГРАМА (SCHEDULE) ---
        private void RefreshSchedule()
        {
            if (_selectedLeagueId == -1) return;
            DataTable dt = _leagueService.GetSchedule(_selectedLeagueId);
            dgvSchedule.DataSource = dt;

            // Скриваме излишните системни колони
            string[] hidden = { "id", "home_team_id", "away_team_id", "league_id" };
            foreach (var colName in hidden)
            {
                if (dgvSchedule.Columns.Contains(colName))
                    dgvSchedule.Columns[colName].Visible = false;
            }

            var cols = dgvSchedule.Columns;

            if (cols.Contains("round_no"))
            {
                cols["round_no"].HeaderText = "Кръг";
                cols["round_no"].DisplayIndex = 0; 
                cols["round_no"].Width = 50;
                cols["round_no"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                cols["round_no"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (cols.Contains("home_team"))
            {
                cols["home_team"].HeaderText = "Домакин";
                cols["home_team"].DisplayIndex = 1; 
                cols["home_team"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (cols.Contains("away_team"))
            {
                cols["away_team"].HeaderText = "Гост";
                cols["away_team"].DisplayIndex = 2; 
                cols["away_team"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (cols.Contains("home_score"))
            {
                cols["home_score"].HeaderText = "ГД";
                cols["home_score"].DisplayIndex = 3; 
                cols["home_score"].Width = 45;
                cols["home_score"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                cols["home_score"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (cols.Contains("away_score"))
            {
                cols["away_score"].HeaderText = "ГГ";
                cols["away_score"].DisplayIndex = 4; 
                cols["away_score"].Width = 45;
                cols["away_score"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                cols["away_score"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (cols.Contains("match_date"))
            {
                cols["match_date"].HeaderText = "Дата";
                cols["match_date"].DisplayIndex = 5; 
                cols["match_date"].Width = 100;
                cols["match_date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                cols["match_date"].DefaultCellStyle.Format = "dd.MM.yyyy";
                cols["match_date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (cols.Contains("is_played"))
            {
                cols["is_played"].HeaderText = "Изигран";
                cols["is_played"].DisplayIndex = 6; 
                cols["is_played"].Width = 80;
                cols["is_played"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }

            dgvSchedule.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void btnGenerateSchedule_Click(object sender, EventArgs e)
        {
            if (_selectedLeagueId == -1) return;
            if (MessageBox.Show("Генериране на нова програма (всяка събота)?", "Потвърждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (_leagueService.GenerateFullSchedule(_selectedLeagueId, out string msg))
                {
                    MessageBox.Show(msg);
                    RefreshSchedule();
                }
                else MessageBox.Show(msg, "Грешка");
            }
        }
    }
}