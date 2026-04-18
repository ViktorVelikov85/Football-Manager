using Football_Manager.BLL;
using Football_Manager.Models;
using System.Data;

namespace Football_Manager.UI
{
    public partial class LeaguesForm : Form
    {
        private readonly LeagueService _leagueService = new LeagueService();
        private int _selectedLeagueId = -1;

        public LeaguesForm()
        {
            InitializeComponent();
        }

        private void LeaguesForm_Load(object sender, EventArgs e)
        {
            SetGridStyle(); 
            LoadLeagues();
        }

        private void LoadLeagues()
        {
            try
            {
                DataTable dt = _leagueService.GetLeagues();
                dgvLeagues.DataSource = dt;
                FormatLeaguesGrid();

                if (dgvLeagues.Rows.Count > 0)
                {
                    dgvLeagues.Rows[0].Selected = true; 

                    SelectLeague(0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при зареждане на лиги: " + ex.Message);
            }
        }
        private void SetGridStyle()
        {
            Font mainFont = new Font("Arial", 12);
            Font boldFont = new Font("Arial", 12, FontStyle.Bold);

            // Общ стил за таблицата с лиги
            dgvLeagues.DefaultCellStyle.Font = mainFont;
            dgvLeagues.ColumnHeadersDefaultCellStyle.Font = boldFont;
            dgvLeagues.RowTemplate.Height = 30;
            dgvLeagues.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLeagues.RowHeadersVisible = false;

            // Общ стил за таблицата с участници
            dgvParticipants.DefaultCellStyle.Font = mainFont;
            dgvParticipants.ColumnHeadersDefaultCellStyle.Font = boldFont;
            dgvParticipants.RowTemplate.Height = 30;
            dgvParticipants.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvParticipants.RowHeadersVisible = false;
        }

        private void FormatLeaguesGrid()
        {
            if (dgvLeagues.Columns.Contains("id")) dgvLeagues.Columns["id"].Visible = false;
            if (dgvLeagues.Columns.Contains("name")) dgvLeagues.Columns["name"].HeaderText = "Лига";
            if (dgvLeagues.Columns.Contains("season")) dgvLeagues.Columns["season"].HeaderText = "Сезон";

            dgvLeagues.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void FormatParticipantsGrid()
        {
            // Ако няма данни, не се опитвай да форматираш
            if (dgvParticipants.CurrentRow == null && dgvParticipants.DataSource == null) return;

            if (dgvParticipants.Columns.Contains("id")) dgvParticipants.Columns["id"].Visible = false;

            if (dgvParticipants.Columns.Contains("name")) dgvParticipants.Columns["name"].HeaderText = "Име на отбор";
            if (dgvParticipants.Columns.Contains("city")) dgvParticipants.Columns["city"].HeaderText = "Град";

            dgvParticipants.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dgvLeagues_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                SelectLeague(e.RowIndex);
            }
        }
        private void SelectLeague(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dgvLeagues.Rows.Count) return;

            var row = dgvLeagues.Rows[rowIndex];
            _selectedLeagueId = Convert.ToInt32(row.Cells["id"].Value);
            txtName.Text = row.Cells["name"].Value.ToString();
            txtSeason.Text = row.Cells["season"].Value.ToString();

            RefreshParticipants();
        }
        private void RefreshParticipants()
        {
            if (_selectedLeagueId == -1) return;

            dgvParticipants.DataSource = _leagueService.GetParticipants(_selectedLeagueId);

            FormatParticipantsGrid();

            DataTable available = _leagueService.GetAvailableClubs(_selectedLeagueId);
            cboAvailableClubs.DataSource = available;
            cboAvailableClubs.DisplayMember = "name";
            cboAvailableClubs.ValueMember = "id";

            if (available.Rows.Count == 0) cboAvailableClubs.Text = "Няма налични отбори";
        }

        private void btnAddLeague_Click(object sender, EventArgs e)
        {
            var league = new League
            {
                Name = txtName.Text.Trim(),
                Season = txtSeason.Text.Trim()
            };

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
        private void btnDeleteLeague_Click(object sender, EventArgs e)
        {
            if (_selectedLeagueId == -1) return;

            if (MessageBox.Show("Сигурни ли сте, че искате да изтриете тази лига и всички нейни участници?",
                "Изтриване", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _leagueService.DeleteLeague(_selectedLeagueId);
                _selectedLeagueId = -1;

                LoadLeagues();

                if (dgvLeagues.Rows.Count == 0)
                {
                    txtName.Clear();
                    txtSeason.Clear();
                    dgvParticipants.DataSource = null;
                    cboAvailableClubs.DataSource = null;
                }
            }
        }
        private void btnUpdateLeague_Click(object sender, EventArgs e)
        {
            if (_selectedLeagueId == -1)
            {
                MessageBox.Show("Моля, първо изберете лига от списъка за редактиране!");
                return;
            }

            var league = new League
            {
                Id = _selectedLeagueId,
                Name = txtName.Text.Trim(),
                Season = txtSeason.Text.Trim()
            };

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

        private void btnClear_Click(object sender, EventArgs e)
        {
            _selectedLeagueId = -1;

            txtName.Clear();
            txtSeason.Clear();
            dgvLeagues.ClearSelection();
            dgvLeagues.CurrentCell = null;
            dgvParticipants.DataSource = null;
            cboAvailableClubs.DataSource = null;

            txtName.Focus();
        }
        private void btnAddClub_Click(object sender, EventArgs e)
        {
            if (_selectedLeagueId == -1)
            {
                MessageBox.Show("Първо изберете лига от списъка вляво!");
                return;
            }

            if (cboAvailableClubs.SelectedValue == null) return;

            int clubId = Convert.ToInt32(cboAvailableClubs.SelectedValue);
            _leagueService.AddClubToLeague(_selectedLeagueId, clubId);
            RefreshParticipants();
        }

        private void btnRemoveClub_Click(object sender, EventArgs e)
        {
            if (_selectedLeagueId == -1 || dgvParticipants.CurrentRow == null) return;

            int clubId = Convert.ToInt32(dgvParticipants.CurrentRow.Cells["id"].Value);
            string clubName = dgvParticipants.CurrentRow.Cells["name"].Value.ToString();

            if (MessageBox.Show($"Премахване на '{clubName}' от лигата?", "Потвърждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _leagueService.RemoveClubFromLeague(_selectedLeagueId, clubId);
                RefreshParticipants();
            }
        }
    }
}