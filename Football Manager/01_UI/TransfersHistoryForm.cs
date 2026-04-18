using Football_Manager.BLL;

namespace Football_Manager.UI
{
    public partial class TransfersHistoryForm : Form
    {
        private readonly PlayerService _playerService = new PlayerService();

        public TransfersHistoryForm()
        {
            InitializeComponent();
        }

        private void TransfersHistoryForm_Load(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            try
            {
                string search = txtSearchNameTransfer.Text.Trim();
                dgvTransfers.DataSource = _playerService.GetTransferHistory(search);
                SetGridStyle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при зареждане на историята: " + ex.Message);
            }
        }

        private void SetGridStyle()
        {
            if (dgvTransfers.DataSource == null) return;

            dgvTransfers.DefaultCellStyle.Font = new Font("Arial", 12);
            dgvTransfers.AlternatingRowsDefaultCellStyle.Font = new Font("Arial", 12);
            dgvTransfers.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 11, FontStyle.Bold);
            dgvTransfers.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
 
            dgvTransfers.RowTemplate.Height = 32;
            foreach (DataGridViewRow row in dgvTransfers.Rows)
            {
                row.Height = 32; 
            }

            // Основни настройки
            dgvTransfers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTransfers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTransfers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvTransfers.ColumnHeadersHeight = 35;


            if (dgvTransfers.Columns.Contains("Такса"))
            {
                dgvTransfers.Columns["Такса"].HeaderText = "Сума (€)";
                dgvTransfers.Columns["Такса"].DefaultCellStyle.Format = "N0";
                dgvTransfers.Columns["Такса"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (dgvTransfers.Columns.Contains("Дата"))
            {
                dgvTransfers.Columns["Дата"].DefaultCellStyle.Format = "dd.MM.yyyy";
                dgvTransfers.Columns["Дата"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void txtSearchNameTransfer_TextChanged(object sender, EventArgs e)
        {
            RefreshGrid();
        }
    }
}