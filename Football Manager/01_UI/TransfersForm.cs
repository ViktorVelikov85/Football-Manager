using Football_Manager.BLL;
using System;
using System.Windows.Forms;

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
            txtFromClub.Text = string.IsNullOrEmpty(currentClubName) ? "Free Agent" : currentClubName;
            txtFromClub.Tag = currentClubId;
        }

        private void TransfersForm_Load(object sender, EventArgs e)
        {
            // Казваме на грида да ползва САМО колоните от Properties Дизайнера
            dgvTransfers.AutoGenerateColumns = false;

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
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при зареждане на клубове: " + ex.Message, "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshGrid()
        {
            try
            {
                string search = txtSearchNameTransfer.Text.Trim();

                // Директно пълним таблицата, Properties се грижи за визията
                dgvTransfers.DataSource = _playerService.GetTransferHistory(search);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при зареждане на трансферите: " + ex.Message, "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearchNameTransfer_TextChanged(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cboToClub.SelectedIndex = -1;
            numFee.Value = 0;
            dtpTransferDate.Value = DateTime.Now;
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            if (_selectedPlayerId == -1)
            {
                MessageBox.Show("Моля, първо изберете играч от мениджмънта на играчи!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboToClub.SelectedIndex == -1)
            {
                MessageBox.Show("Моля, изберете нов клуб!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int toClubId = Convert.ToInt32(cboToClub.SelectedValue);
            int? fromClubId = txtFromClub.Tag as int?;

            if (fromClubId.HasValue && fromClubId.Value == toClubId)
            {
                MessageBox.Show("Играчът вече е в този клуб! Изберете различен отбор.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal fee = numFee.Value;
            DateTime transferDate = dtpTransferDate.Value;

            try
            {
                // Изпълняваме трансфера
                _playerService.ExecuteTransfer(_selectedPlayerId, fromClubId, toClubId, transferDate, fee);

                MessageBox.Show("Трансферът беше извършен успешно!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Опресняваме долната таблица веднага
                RefreshGrid();
                btnClear_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при изпълнение на трансфера: " + ex.Message, "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}