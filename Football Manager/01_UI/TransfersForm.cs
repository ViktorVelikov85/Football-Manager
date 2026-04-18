using Football_Manager.BLL;

namespace Football_Manager.UI
{
    public partial class TransfersForm : Form
    {
        private readonly PlayerService _playerService = new PlayerService();
        private readonly ClubService _clubService = new ClubService();
        private int _selectedPlayerId = -1;

        public TransfersForm()
        {
            InitializeComponent();
        }

        public TransfersForm(int playerId, string playerName, int? currentClubId, string currentClubName)
        {
            InitializeComponent();
            _selectedPlayerId = playerId;
            txtPlayer.Text = playerName;
            txtFromClub.Text = string.IsNullOrEmpty(currentClubName) ? "Свободен агент" : currentClubName;
            txtFromClub.Tag = currentClubId;
        }

        private void TransfersForm_Load(object sender, EventArgs e)
        {

            LoadClubs();
            RefreshGrid();

            txtPlayer.ReadOnly = true;
            txtFromClub.ReadOnly = true;
        }

        private void LoadClubs()
        {
            try
            {
                cboToClub.DataSource = _clubService.GetAllClubs();
                cboToClub.DisplayMember = "name";
                cboToClub.ValueMember = "id";
                cboToClub.SelectedIndex = -1;
            }
            catch (Exception ex) { MessageBox.Show("Грешка при зареждане на клубове: " + ex.Message); }
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            if (_selectedPlayerId == -1)
            {
                MessageBox.Show("Изберете играч от списъка с играчи първо!");
                return;
            }

            if (cboToClub.SelectedValue == null)
            {
                MessageBox.Show("Изберете нов клуб!");
                return;
            }

            int toClubId = Convert.ToInt32(cboToClub.SelectedValue);
            int? fromClubId = txtFromClub.Tag as int?;

            if (fromClubId != null && fromClubId == toClubId)
            {
                MessageBox.Show("Играчът вече е в този клуб!");
                return;
            }

            try
            {
                _playerService.ExecuteTransfer(_selectedPlayerId, fromClubId, toClubId, dtpTransferDate.Value, numFee.Value);
                MessageBox.Show("Трансферът е извършен успешно!");

                txtFromClub.Text = cboToClub.Text;
                txtFromClub.Tag = toClubId;
                RefreshGrid();

                cboToClub.SelectedIndex = -1;
                numFee.Value = 0;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void RefreshGrid()
        {
            dgvTransfers.DataSource = _playerService.GetTransferHistory(txtSearchNameTransfer.Text);
            SetGridStyle();
        }

        private void SetGridStyle()
        {
            if (dgvTransfers.DataSource == null) return;

            // 1. Само шрифт за таблицата - Arial 12
            Font gridFont = new Font("Arial", 12);
            dgvTransfers.DefaultCellStyle.Font = gridFont;
            dgvTransfers.AlternatingRowsDefaultCellStyle.Font = gridFont;
            dgvTransfers.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);

            // 2. Настройки за разположението
            dgvTransfers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTransfers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTransfers.RowHeadersVisible = false;
            dgvTransfers.RowTemplate.Height = 30;

            // 3. Форматиране на колоните (ако съществуват в твоя SQL изглед)
            if (dgvTransfers.Columns.Contains("Такса"))
            {
                dgvTransfers.Columns["Такса"].DefaultCellStyle.Format = "N0";
                dgvTransfers.Columns["Такса"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (dgvTransfers.Columns.Contains("Дата"))
            {
                dgvTransfers.Columns["Дата"].DefaultCellStyle.Format = "dd.MM.yyyy";
            }

            // Принудително опресняване на подредбата
            dgvTransfers.Refresh();
        }

        private void txtSearchNameTransfer_TextChanged(object sender, EventArgs e) => RefreshGrid();

        private void btnClear_Click(object sender, EventArgs e)
        {
            cboToClub.SelectedIndex = -1;
            numFee.Value = 0;
            dtpTransferDate.Value = DateTime.Now;
            txtSearchNameTransfer.Clear();
            RefreshGrid();
        }
    }
}