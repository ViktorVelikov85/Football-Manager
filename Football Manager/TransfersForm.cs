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
            SetGridStyle(); 
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
        private void SetGridStyle()
        {
            // 1. Основни настройки за шрифта (както в PlayersForm)
            Font commonFont = new Font("Arial", 12);
            dgvTransfers.DefaultCellStyle.Font = commonFont;
            dgvTransfers.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dgvTransfers.RowsDefaultCellStyle.Font = commonFont;
            dgvTransfers.AlternatingRowsDefaultCellStyle.Font = commonFont;

            // 2. Общи настройки на таблицата
            dgvTransfers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTransfers.AllowUserToAddRows = false;
            dgvTransfers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTransfers.ReadOnly = true;
            dgvTransfers.RowHeadersVisible = false;

            // 1. Правим цветовете на селекцията същите като обикновените цветове.
            // Така дори и потребителят да кликне, той няма да види промяна (няма да "светне").
            dgvTransfers.DefaultCellStyle.SelectionBackColor = dgvTransfers.DefaultCellStyle.BackColor;
            dgvTransfers.DefaultCellStyle.SelectionForeColor = dgvTransfers.DefaultCellStyle.ForeColor;

            if (dgvTransfers.Columns["Такса"] != null)
            {
                dgvTransfers.Columns["Такса"].HeaderText = "Такса €";

                // Подравняваме вдясно за по-добра четимост
                dgvTransfers.Columns["Такса"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvTransfers.Columns["Такса"].DefaultCellStyle.Format = "N2";
            }
            if (dgvTransfers.Columns["Дата"] != null)
            {
                dgvTransfers.Columns["Дата"].DefaultCellStyle.Format = "dd.MM.yyyy";
            }

            dgvTransfers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void RefreshGrid()
        {
            dgvTransfers.DataSource = transferRepo.GetTransfers(txtSearchNameTransfer.Text);
            SetGridStyle(); 
        }
        private void txtSearchNameTransfer_TextChanged(object sender, EventArgs e)
        {
            string search = txtSearchNameTransfer.Text;
            dgvTransfers.DataSource = transferRepo.GetTransfers(search);
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

        private void ClearFields()
        {
            cboPlayer.SelectedIndex = -1;      // Връща комбобокса в начално (празно) състояние
            txtFromClub.Clear();               // Изчиства текстовото поле за текущия клуб
            txtFromClub.Tag = null;            // Изчиства и скритото ID в Tag-а
            cboToClub.SelectedIndex = -1;      // Изчиства избора на нов клуб
            numFee.Value = 0;                  // Нулира сумата
            txtSearchNameTransfer.Clear();     // Изчиства търсачката
            dtpTransferDate.Value = DateTime.Now;

            dgvTransfers.ClearSelection();
        }
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
                ClearFields();

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
            ClearFields();
        }
    }
}