using Football_Manager;
using System;
using System.Data;
using System.Windows.Forms;

namespace Football_Manager
{
    public partial class TransfersForm : Form
    {
        // Инициализиране на репозиторитата
        private TransfersRepository transferRepo = new TransfersRepository();
        private PlayersRepository playerRepo = new PlayersRepository();
        private ClubsRepository clubRepo = new ClubsRepository();

        public TransfersForm()
        {
            InitializeComponent();
        }

        private void TransfersForm_Load(object sender, EventArgs e)
        {
            LoadInitialData();
        }

        private void LoadInitialData()
        {
            try
            {
                // 1. Зареждане на играчите
                DataTable players = playerRepo.GetPlayers();
                cboPlayer.DataSource = players;
                cboPlayer.DisplayMember = "full_name";
                cboPlayer.ValueMember = "id";
                cboPlayer.SelectedIndex = -1; // Да няма избран по подразбиране

                // 2. Зареждане на клубовете (целеви клуб)
                DataTable clubs = clubRepo.GetAllClubs();
                cboToClub.DataSource = clubs;
                cboToClub.DisplayMember = "name";
                cboToClub.ValueMember = "id";
                cboToClub.SelectedIndex = -1;

                // 3. Зареждане на историята в DataGridView
                RefreshGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при зареждане на данни: " + ex.Message, "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshGrid()
        {
            dgvTransfers.DataSource = transferRepo.GetTransfers();
            // Настройка на автоматично преоразмеряване за по-добър вид
            dgvTransfers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void cboPlayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPlayer.SelectedValue != null && cboPlayer.SelectedItem is DataRowView row)
            {
                // Проверяваме дали колоната съществува в DataTable
                if (row.DataView.Table.Columns.Contains("club_id"))
                {
                    txtFromClub.Tag = row["club_id"];

                    // Проверяваме и за името на клуба
                    if (row.DataView.Table.Columns.Contains("club_name"))
                    {
                        txtFromClub.Text = row["club_name"].ToString();
                    }
                }
                else
                {
                    // Ако влезеш тук, значи SQL заявката ти в Repository-то не връща club_id
                    MessageBox.Show("Грешка: Колоната 'club_id' липсва в заредените данни!");
                }
            }
        }

        // БУТОН: ИЗПЪЛНЕНИЕ НА ТРАНСФЕР
        private void btnTransfer_Click(object sender, EventArgs e)
        {
            // 1. Валидация за селекция
            if (cboPlayer.SelectedValue == null || cboToClub.SelectedValue == null)
            {
                MessageBox.Show("Моля, изберете играч и нов клуб!", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int playerId = (int)cboPlayer.SelectedValue;
            int toClubId = (int)cboToClub.SelectedValue;

            // Вземаме ID-то на текущия клуб от Tag-а (който запълнихме в SelectedIndexChanged)
            int currentClubId = txtFromClub.Tag != DBNull.Value ? Convert.ToInt32(txtFromClub.Tag) : -1;

            // 2. КЛЮЧОВА ВАЛИДАЦИЯ: "Не към същия клуб"
            if (currentClubId == toClubId)
            {
                MessageBox.Show($"Играчът вече е в отбора на {cboToClub.Text}! Изберете друг клуб.",
                                "Невалиден трансфер", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3. Изпълнение на трансфера
            try
            {
                transferRepo.AddTransfer(playerId, currentClubId == -1 ? (int?)null : currentClubId, toClubId, dtpTransferDate.Value, numFee.Value);
                MessageBox.Show("Трансферът е извършен успешно!");
                RefreshGrid();

                // Опресняваме txtFromClub, защото играчът вече е в новия клуб
                txtFromClub.Text = cboToClub.Text;
                txtFromClub.Tag = toClubId;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка: " + ex.Message);
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            cboPlayer.SelectedIndex = -1;
            cboToClub.SelectedIndex = -1;
            txtFromClub.Clear();
            txtFromClub.Tag = null;
            numFee.Value = 0;
            dtpTransferDate.Value = DateTime.Now;
        }
    }
}