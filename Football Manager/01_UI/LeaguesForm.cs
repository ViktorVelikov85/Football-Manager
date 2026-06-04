using Football_Manager.BLL;
using Football_Manager.Models;
using System;
using System.Data;
using System.Windows.Forms;

namespace Football_Manager.UI
{
    public partial class LeaguesForm : Form
    {
        private readonly LeaguesService _leagueService = new LeaguesService();
        private int _selectedLeagueId = -1;
        private readonly ToolTip _toolTip = new ToolTip();

        public LeaguesForm()
        {
            InitializeComponent();
        }

        // Жизнен цикъл на формата
        private void LeaguesForm_Load(object sender, EventArgs e)
        {
            // Спираме автоматичното генериране, тъй като колоните и подравняванията са изцяло в Properties
            dgvLeagues.AutoGenerateColumns = false;
            dgvParticipants.AutoGenerateColumns = false;
            dgvSchedule.AutoGenerateColumns = false;

            LoadLeagues();

            _toolTip.SetToolTip(btnManageMatch, "Може също така да щракнете два пъти върху мач в таблицата, за да го отворите директно.");
            _toolTip.IsBalloon = true;
        }

        // Управление на лиги
        private void LoadLeagues()
        {
            try
            {
                DataTable dt = _leagueService.GetLeagues();
                dgvLeagues.DataSource = dt;

                if (dgvLeagues.Rows.Count > 0)
                    SelectLeague(0);
                else
                    ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при зареждане на лиги: " + ex.Message);
            }
        }

        private void SelectLeague(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dgvLeagues.Rows.Count) return;

            var row = dgvLeagues.Rows[rowIndex];
            var dataRow = (DataRowView)row.DataBoundItem;

            _selectedLeagueId = Convert.ToInt32(dataRow["id"]);
            txtName.Text = dataRow["name"]?.ToString();
            txtSeason.Text = dataRow["season"]?.ToString();

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
            else
            {
                MessageBox.Show(msg, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
            else
            {
                MessageBox.Show(msg, "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteLeague_Click(object sender, EventArgs e)
        {
            if (_selectedLeagueId == -1) return;

            if (MessageBox.Show("Изтриване на лигата и всички мачове към нея?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _leagueService.DeleteLeague(_selectedLeagueId);
                _selectedLeagueId = -1;
                LoadLeagues();
            }
        }

        // Управление на участници (клубове в лигата)
        private void RefreshParticipants()
        {
            if (_selectedLeagueId == -1) return;

            dgvParticipants.DataSource = _leagueService.GetParticipants(_selectedLeagueId);

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

            var partRow = (DataRowView)dgvParticipants.CurrentRow.DataBoundItem;
            int clubId = Convert.ToInt32(partRow["id"]);

            _leagueService.RemoveClubFromLeague(_selectedLeagueId, clubId);
            RefreshParticipants();
        }

        // Генератор и управление на срещи
        private void RefreshSchedule()
        {
            if (_selectedLeagueId == -1) return;
            dgvSchedule.DataSource = _leagueService.GetSchedule(_selectedLeagueId);
        }

        private void btnGenerateSchedule_Click(object sender, EventArgs e)
        {
            if (_selectedLeagueId == -1) return;

            if (MessageBox.Show("Генериране на нова програма (всяка събота)? Старата ще се изтрие!", "Потвърждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (_leagueService.GenerateFullSchedule(_selectedLeagueId, out string msg))
                {
                    MessageBox.Show(msg, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshSchedule();
                }
                else
                {
                    MessageBox.Show(msg, "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvSchedule_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) OpenMatchManagement();
        }

        private void btnManageMatch_Click(object sender, EventArgs e)
        {
            OpenMatchManagement();
        }

        // Помощни методи
        private void OpenMatchManagement()
        {
            if (dgvSchedule.CurrentRow == null)
            {
                MessageBox.Show("Моля, първо изберете мач от програмата!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Извличане на скритите релационни ID ключове от DataTable
            var matchRow = (DataRowView)dgvSchedule.CurrentRow.DataBoundItem;
            int matchId = Convert.ToInt32(matchRow["id"]);
            int homeTeamId = Convert.ToInt32(matchRow["home_team_id"]);
            int awayTeamId = Convert.ToInt32(matchRow["away_team_id"]);

            string homeTeamName = dgvSchedule.CurrentRow.Cells["colHomeTeam"].Value?.ToString() ?? "";
            string awayTeamName = dgvSchedule.CurrentRow.Cells["colAwayTeam"].Value?.ToString() ?? "";

            DateTime matchDate = matchRow["match_date"] != DBNull.Value
                ? Convert.ToDateTime(matchRow["match_date"])
                : DateTime.Today;

            using (MatchManagementForm frm = new MatchManagementForm(matchId, homeTeamId, awayTeamId, homeTeamName, awayTeamName, matchDate))
            {
                frm.ShowDialog();
                RefreshSchedule();
            }
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

        private void btnClear_Click(object sender, EventArgs e) => ClearInputs();
        
    }
}