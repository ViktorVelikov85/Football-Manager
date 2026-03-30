using Football_Manager;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Football_Manager
{
    public partial class TransfersForm : Form
    {
        private TransfersRepository transferRepo = new TransfersRepository();
        private ClubsRepository clubRepo = new ClubsRepository();
        
        // Това ID се пълни само при отваряне на формата от PlayersForm
        private int _selectedPlayerId = -1;

        // Празен конструктор (ако се отвори от главното меню за преглед)
        public TransfersForm()
        {
            InitializeComponent();
        }

        // Конструктор за иницииране на трансфер от PlayersForm
        public TransfersForm(int playerId, string playerName, int? currentClubId, string currentClubName)
        {
            InitializeComponent();

            _selectedPlayerId = playerId;
            
            // Задаваме стойностите, които сме получили
            txtPlayer.Text = playerName;
            txtFromClub.Text = string.IsNullOrEmpty(currentClubName) ? "Свободен агент" : currentClubName;
            txtFromClub.Tag = currentClubId;
        }

        private void TransfersForm_Load(object sender, EventArgs e)
        {
            LoadClubs();
            RefreshGrid();
            
            // За по-голяма сигурност: правим полетата за име и стар клуб само за четене
            txtPlayer.ReadOnly = true;
            txtFromClub.ReadOnly = true;
        }

        private void LoadClubs()
        {
            try
            {
                DataTable clubs = clubRepo.GetAllClubs();
                cboToClub.DataSource = clubs;
                cboToClub.DisplayMember = "name";
                cboToClub.ValueMember = "id";
                cboToClub.SelectedIndex = -1;
            }
            catch (Exception ex) { MessageBox.Show("Грешка при клубове: " + ex.Message); }
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            // Проверка дали изобщо е зареден играч от PlayersForm
            if (_selectedPlayerId == -1)
            {
                MessageBox.Show("Моля, отворете тази форма през списъка с играчи, за да изберете кой да бъде трансфериран.", "Инфо");
                return;
            }

            if (cboToClub.SelectedValue == null)
            {
                MessageBox.Show("Изберете нов клуб!");
                return;
            }

            int toClubId = (int)cboToClub.SelectedValue;
            int? fromClubId = txtFromClub.Tag as int?;

            if (fromClubId.HasValue && fromClubId.Value == toClubId)
            {
                MessageBox.Show("Играчът вече е в този клуб!");
                return;
            }

            try
            {
                transferRepo.AddTransfer(_selectedPlayerId, fromClubId, toClubId, dtpTransferDate.Value, numFee.Value);
                MessageBox.Show("Трансферът е успешен!");
                
                RefreshGrid();
                
                // След трансфера "новият" клуб става "стар"
                txtFromClub.Text = cboToClub.Text;
                txtFromClub.Tag = toClubId;
                
                // Нулираме избора за следващ запис
                cboToClub.SelectedIndex = -1;
                numFee.Value = 0;
            }
            catch (Exception ex) { MessageBox.Show("Грешка: " + ex.Message); }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // Изчистваме само входните полета, които потребителят попълва тук
            cboToClub.SelectedIndex = -1;
            numFee.Value = 0;
            dtpTransferDate.Value = DateTime.Now;
            txtSearchNameTransfer.Clear();
            
            // Важно: НЕ изчистваме _selectedPlayerId и txtPlayer, 
            // защото те идват от предната форма и не трябва да се губят.
        }

        private void RefreshGrid()
        {
            dgvTransfers.DataSource = transferRepo.GetTransfers(txtSearchNameTransfer.Text);
            SetGridStyle();
        }

        private void SetGridStyle()
        {
            if (dgvTransfers.DataSource == null) return;

            dgvTransfers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTransfers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTransfers.AllowUserToAddRows = false;
            dgvTransfers.ReadOnly = true;
            dgvTransfers.RowHeadersVisible = false;

            Font commonFont = new Font("Arial", 12);
            dgvTransfers.DefaultCellStyle.Font = commonFont;
            dgvTransfers.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            // Спираме автоматичното избиране на първия ред, за да е по-чисто
            dgvTransfers.ClearSelection();

            if (dgvTransfers.Columns["Такса"] != null)
                dgvTransfers.Columns["Такса"].DefaultCellStyle.Format = "N2";
            
            if (dgvTransfers.Columns["Дата"] != null)
                dgvTransfers.Columns["Дата"].DefaultCellStyle.Format = "dd.MM.yyyy";
        }

        private void txtSearchNameTransfer_TextChanged(object sender, EventArgs e) => RefreshGrid();
    }
}