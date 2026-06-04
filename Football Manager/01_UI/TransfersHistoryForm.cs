using Football_Manager.BLL;
using System;
using System.Windows.Forms;

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
            // Казваме на грида да ползва САМО и единствено колоните от Properties дизайнера
            dgvTransfers.AutoGenerateColumns = false;

            RefreshGrid();
        }

        private void RefreshGrid()
        {
            try
            {
                string search = txtSearchNameTransfer.Text.Trim();

                // Закачаме данните от базата
                dgvTransfers.DataSource = _playerService.GetTransferHistory(search);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при зареждане на историята: " + ex.Message, "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearchNameTransfer_TextChanged(object sender, EventArgs e)
        {
            // Търсене в реално време при всяка промяна на текста
            RefreshGrid();
        }
    }
}