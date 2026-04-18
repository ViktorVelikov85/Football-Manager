using Football_Manager.BLL;
using Football_Manager.Models;
using System.Data;

namespace Football_Manager.UI
{
    public partial class PlayersForm : Form
    {
        private readonly PlayerService _playerService = new PlayerService();
        private readonly ClubService _clubService = new ClubService();
        private int _selectedPlayerId = -1;

        public PlayersForm()
        {
            InitializeComponent();
        }

        private void PlayersForm_Load(object sender, EventArgs e)
        {
            SetupComboBoxes();
            LoadPlayers();
            ClearInputs();

            string positionLegend = "GK - Вратар (Goalkeeper)\n" +
                                    "DF - Защитник (Defender)\n" +
                                    "MF - Полузащитник (Midfielder)\n" +
                                    "FW - Нападател (Forward)";

            ToolTip positionToolTip = new ToolTip();

            positionToolTip.SetToolTip(cboPosition, positionLegend);
            positionToolTip.SetToolTip(cboFilterPosition, positionLegend);
        }

        private void SetupComboBoxes()
        {
            try
            {
                // Позиции и Статус
                cboPosition.DataSource = new List<string> { "GK", "DF", "MF", "FW" };
                cboStatus.DataSource = new List<string> { "Active", "Injured", "Suspended" };
                cboFilterPosition.DataSource = new List<string> { "Всички", "GK", "DF", "MF", "FW" };

                // Зареждане на клубове
                DataTable clubs = _clubService.GetAllClubs();

                // За ComboBox във формата
                cboClub.DisplayMember = "name";
                cboClub.ValueMember = "id";
                cboClub.DataSource = clubs;

                // За ComboBox за филтриране
                DataTable filterClubs = clubs.Copy();
                DataRow row = filterClubs.NewRow();
                row["id"] = 0;
                row["name"] = "Всички";
                filterClubs.Rows.InsertAt(row, 0);

                cboFilterClub.DisplayMember = "name";
                cboFilterClub.ValueMember = "id";
                cboFilterClub.DataSource = filterClubs;
            }
            catch (Exception ex) { MessageBox.Show("Грешка при инициализация: " + ex.Message); }
        }

        private void LoadPlayers()
        {
            try
            {
                dgvPlayers.DataSource = _playerService.GetPlayers();
                SetGridStyle();
            }
            catch (Exception ex) { MessageBox.Show("Грешка при зареждане: " + ex.Message); }
        }

        private void SetGridStyle()
        {
            if (dgvPlayers.DataSource == null) return;

            Font mainFont = new Font("Arial", 12);
            dgvPlayers.DefaultCellStyle.Font = mainFont;
            dgvPlayers.AlternatingRowsDefaultCellStyle.Font = mainFont;
            dgvPlayers.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);

            dgvPlayers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPlayers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPlayers.RowHeadersVisible = false;
            dgvPlayers.RowTemplate.Height = 30;

            var headers = new Dictionary<string, string>
            {
                { "id", "ID" }, { "full_name", "Име на играч" }, { "club_name", "Отбор" },
                { "position", "Позиция" }, { "shirt_number", "№" },
                { "birth_date", "Роден на" }, { "status", "Статус" }
            };

            foreach (var header in headers)
            {
                if (dgvPlayers.Columns.Contains(header.Key))
                    dgvPlayers.Columns[header.Key].HeaderText = header.Value;
            }

            if (dgvPlayers.Columns.Contains("club_id")) dgvPlayers.Columns["club_id"].Visible = false;

            if (dgvPlayers.Columns.Contains("id"))
            {
                dgvPlayers.Columns["id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgvPlayers.Columns["id"].Width = 60;
            }

            if (dgvPlayers.Columns.Contains("full_name"))
            {
                dgvPlayers.Columns["full_name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgvPlayers.Columns["full_name"].Width = 200;
            }

            if (dgvPlayers.Columns.Contains("shirt_number"))
            {
                dgvPlayers.Columns["shirt_number"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgvPlayers.Columns["shirt_number"].Width = 50;
            }

            if (dgvPlayers.Columns.Contains("birth_date"))
                dgvPlayers.Columns["birth_date"].DefaultCellStyle.Format = "dd.MM.yyyy";
        }

        private void ApplyFilters()
        {
            int? clubId = (cboFilterClub.SelectedValue is int cid && cid > 0) ? cid : (int?)null;
            string pos = cboFilterPosition.SelectedItem?.ToString() ?? "Всички";
            dgvPlayers.DataSource = _playerService.GetPlayers(clubId, pos, txtSearchName.Text.Trim());
        }
        private void btnClearFilters_Click(object sender, EventArgs e)
        {
            txtSearchName.Clear();
            if (cboFilterClub.Items.Count > 0) cboFilterClub.SelectedIndex = 0;
            if (cboFilterPosition.Items.Count > 0) cboFilterPosition.SelectedIndex = 0;

            ApplyFilters();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;
            var player = MapInputsToModel();

            if (_playerService.SavePlayer(player, true, out string message))
            {
                MessageBox.Show(message);
                LoadPlayers();
                ClearInputs();
            }
            else MessageBox.Show(message);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedPlayerId == -1) { MessageBox.Show("Изберете играч!"); return; }
            if (!ValidateInputs()) return;

            var player = MapInputsToModel();
            player.Id = _selectedPlayerId;

            if (_playerService.SavePlayer(player, false, out string message))
            {
                MessageBox.Show(message);
                LoadPlayers();
                ClearInputs();
            }
            else MessageBox.Show(message);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedPlayerId == -1) return;
            if (MessageBox.Show("Изтриване на играча?", "Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _playerService.DeletePlayer(_selectedPlayerId);
                LoadPlayers();
                ClearInputs();
            }
        }

        private void dgvPlayers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dgvPlayers.Rows[e.RowIndex];

            _selectedPlayerId = Convert.ToInt32(row.Cells["id"].Value);
            string fullName = row.Cells["full_name"].Value?.ToString() ?? "";
            var names = fullName.Split(new[] { ' ' }, 2);
            txtFirstName.Text = names[0];
            txtLastName.Text = names.Length > 1 ? names[1] : "";

            cboPosition.SelectedItem = row.Cells["position"].Value?.ToString();
            numShirtNumber.Value = Convert.ToDecimal(row.Cells["shirt_number"].Value);
            cboStatus.SelectedItem = row.Cells["status"].Value?.ToString();
            dtpBirthDate.Value = Convert.ToDateTime(row.Cells["birth_date"].Value);
            cboClub.SelectedValue = row.Cells["club_id"].Value;
        }

        private Player MapInputsToModel() => new Player
        {
            FullName = $"{txtFirstName.Text.Trim()} {txtLastName.Text.Trim()}",
            ClubId = Convert.ToInt32(cboClub.SelectedValue),
            BirthDate = dtpBirthDate.Value,
            Position = cboPosition.SelectedItem.ToString(),
            ShirtNumber = (int)numShirtNumber.Value,
            Status = cboStatus.SelectedItem.ToString()
        };

        private bool ValidateInputs()
        {
            if (cboPosition.SelectedItem == null || cboClub.SelectedValue == null || cboStatus.SelectedItem == null)
            {
                MessageBox.Show("Моля, направете избор от падащите менюта!");
                return false;
            }
            return true;
        }

        private void ClearInputs()
        {
            txtFirstName.Clear(); txtLastName.Clear();
            numShirtNumber.Value = 1;
            dtpBirthDate.Value = DateTime.Now;
            cboPosition.SelectedIndex = -1;
            cboClub.SelectedIndex = -1;
            _selectedPlayerId = -1;
            dgvPlayers.ClearSelection();
        }

        private void txtSearchName_TextChanged(object sender, EventArgs e) => ApplyFilters();
        private void cboFilterClub_SelectedIndexChanged(object sender, EventArgs e) => ApplyFilters();
        private void cboFilterPosition_SelectedIndexChanged(object sender, EventArgs e) => ApplyFilters();
        private void btnClear_Click(object sender, EventArgs e) => ClearInputs();

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            if (dgvPlayers.CurrentRow == null) return;
            var row = dgvPlayers.CurrentRow;

            int pId = Convert.ToInt32(row.Cells["id"].Value);
            string pName = row.Cells["full_name"].Value.ToString();
            int? cId = row.Cells["club_id"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["club_id"].Value) : (int?)null;
            string cName = row.Cells["club_name"].Value?.ToString() ?? "Свободен агент";

            TransfersForm transferFrm = new TransfersForm(pId, pName, cId, cName);
            transferFrm.ShowDialog();
            LoadPlayers();
        }
    }
}