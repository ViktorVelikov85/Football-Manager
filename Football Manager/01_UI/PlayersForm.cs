using Football_Manager.BLL;
using Football_Manager.Models;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

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

        // Жизнен цикъл на формата
        private void PlayersForm_Load(object sender, EventArgs e)
        {
            // Използваме изцяло твоите ръчно конфигурирани колони от Properties панела
            dgvPlayers.AutoGenerateColumns = false;

            LoadClubsData();
            LoadPlayers();
            ClearInputs();

            // По подразбиране филтърът за позиция отива на "Всички"
            if (cboFilterPosition.Items.Contains("Всички"))
            {
                cboFilterPosition.SelectedItem = "Всички";
            }
            else if (cboFilterPosition.Items.Count > 0)
            {
                cboFilterPosition.SelectedIndex = 0;
            }

            string positionLegend = "GK - Вратар (Goalkeeper)\n" +
                                    "DF - Защитник (Defender)\n" +
                                    "MF - Полузащитник (Midfielder)\n" +
                                    "FW - Нападател (Forward)";

            ToolTip positionToolTip = new ToolTip();
            positionToolTip.SetToolTip(cboPosition, positionLegend);
            positionToolTip.SetToolTip(cboFilterPosition, positionLegend);
        }

        // Динамично зареждане на клубовете от базата
        private void LoadClubsData()
        {
            try
            {
                DataTable clubs = _clubService.GetAllClubs();

                // Зареждане на клубове в основното меню
                cboClub.DisplayMember = "name";
                cboClub.ValueMember = "id";
                cboClub.DataSource = clubs;

                // Зареждане на клубове във филтъра с опция "Всички"
                DataTable filterClubs = clubs.Copy();
                DataRow row = filterClubs.NewRow();
                row["id"] = 0;
                row["name"] = "Всички";
                filterClubs.Rows.InsertAt(row, 0);

                cboFilterClub.DisplayMember = "name";
                cboFilterClub.ValueMember = "id";
                cboFilterClub.DataSource = filterClubs;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при зареждане на клубове: " + ex.Message, "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPlayers()
        {
            try
            {
                dgvPlayers.DataSource = _playerService.GetPlayers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при зареждане на играчи: " + ex.Message, "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Автоматичен превод и оцветяване в реално време (Закачено към Events -> CellFormatting)
        private void dgvPlayers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvPlayers.Columns[e.ColumnIndex].DataPropertyName == "status" && e.Value != null)
            {
                string statusInDb = e.Value.ToString();
                DataGridViewRow row = dgvPlayers.Rows[e.RowIndex];

                // 1. Превеждаме думата визуално за потребителя
                if (statusInDb == "Active" || statusInDb == "Активен")
                {
                    e.Value = "Активен";
                    row.DefaultCellStyle.BackColor = Color.White;
                }
                else if (statusInDb == "Injured" || statusInDb == "Контузен")
                {
                    e.Value = "Контузен";
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 220, 220); // Меко пастелно червено
                }
                else if (statusInDb == "Suspended" || statusInDb == "Наказан")
                {
                    e.Value = "Наказан";
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 200); // Меко пастелно жълто
                }

                e.FormattingApplied = true; // Казваме на Windows Forms, че сме форматирали клетката
            }
        }

        // Операции с данни (CRUD)
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            var player = MapInputsToModel();

            if (_playerService.SavePlayer(player, true, out string message))
            {
                MessageBox.Show(message, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadPlayers();
                ClearInputs();
            }
            else
            {
                MessageBox.Show(message, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedPlayerId == -1)
            {
                MessageBox.Show("Моля, изберете играч от таблицата!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputs()) return;

            var player = MapInputsToModel();
            player.Id = _selectedPlayerId;

            if (_playerService.SavePlayer(player, false, out string message))
            {
                MessageBox.Show(message, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadPlayers();
                ClearInputs();
            }
            else
            {
                MessageBox.Show(message, "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedPlayerId == -1)
            {
                MessageBox.Show("Моля, изберете играч за изтриване!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Сигурни ли сте, че искате да изтриете този играч?", "Потвърждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _playerService.DeletePlayer(_selectedPlayerId);
                LoadPlayers();
                ClearInputs();
            }
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            if (dgvPlayers.CurrentRow == null) return;

            var dataRow = (DataRowView)dgvPlayers.CurrentRow.DataBoundItem;

            int pId = Convert.ToInt32(dataRow["id"]);
            string pName = dataRow["full_name"].ToString();
            int? cId = dataRow["club_id"] != DBNull.Value ? Convert.ToInt32(dataRow["club_id"]) : (int?)null;
            string cName = dataRow["club_name"]?.ToString() ?? "Свободен агент";

            using (TransfersForm transferFrm = new TransfersForm(pId, pName, cId, cName))
            {
                transferFrm.ShowDialog();
                LoadPlayers();
            }
        }

        // Избор на ред от таблицата и зареждане в контролите
        private void dgvPlayers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var dataRow = (DataRowView)dgvPlayers.Rows[e.RowIndex].DataBoundItem;

            _selectedPlayerId = Convert.ToInt32(dataRow["id"]);
            string fullName = dataRow["full_name"]?.ToString() ?? "";

            var names = fullName.Split(new[] { ' ' }, 2);
            txtFirstName.Text = names[0];
            txtLastName.Text = names.Length > 1 ? names[1] : "";

            cboPosition.SelectedItem = dataRow["position"]?.ToString();
            numShirtNumber.Value = Convert.ToDecimal(dataRow["shirt_number"]);
            dtpBirthDate.Value = Convert.ToDateTime(dataRow["birth_date"]);

            // Мапваме английския статус от базата към българския избор в твоя ComboBox
            string dbStatus = dataRow["status"]?.ToString();
            if (dbStatus == "Active") cboStatus.SelectedItem = "Активен";
            else if (dbStatus == "Injured") cboStatus.SelectedItem = "Контузен";
            else if (dbStatus == "Suspended") cboStatus.SelectedItem = "Наказан";
            else cboStatus.SelectedItem = dbStatus; // За всеки случай, ако вече има български записи

            if (dataRow["club_id"] != DBNull.Value)
                cboClub.SelectedValue = dataRow["club_id"];
            else
                cboClub.SelectedIndex = -1;
        }

        private void ApplyFilters()
        {
            int? clubId = (cboFilterClub.SelectedValue is int cid && cid > 0) ? cid : (int?)null;
            string pos = cboFilterPosition.SelectedItem?.ToString() ?? "Всички";

            dgvPlayers.DataSource = _playerService.GetPlayers(clubId, pos, txtSearchName.Text.Trim());
        }

        private void txtSearchName_TextChanged(object sender, EventArgs e) => ApplyFilters();

        private void cboFilterClub_SelectedIndexChanged(object sender, EventArgs e) => ApplyFilters();

        private void cboFilterPosition_SelectedIndexChanged(object sender, EventArgs e) => ApplyFilters();

        private void btnClearFilters_Click(object sender, EventArgs e)
        {
            txtSearchName.Clear();
            if (cboFilterClub.Items.Count > 0) cboFilterClub.SelectedIndex = 0;
            if (cboFilterPosition.Items.Contains("Всички")) cboFilterPosition.SelectedItem = "Всички";
            else if (cboFilterPosition.Items.Count > 0) cboFilterPosition.SelectedIndex = 0;

            ApplyFilters();
        }

        // Преобразуване към модела: тук конвертираме българските селекции обратно към английски за базата
        private Player MapInputsToModel()
        {
            string uiStatus = cboStatus.SelectedItem?.ToString() ?? "Активен";
            string dbStatus = "Active"; // стойност по подразбиране

            if (uiStatus == "Контузен") dbStatus = "Injured";
            else if (uiStatus == "Наказан") dbStatus = "Suspended";

            return new Player
            {
                FullName = $"{txtFirstName.Text.Trim()} {txtLastName.Text.Trim()}",
                ClubId = cboClub.SelectedValue != null ? Convert.ToInt32(cboClub.SelectedValue) : 0,
                BirthDate = dtpBirthDate.Value,
                Position = cboPosition.SelectedItem?.ToString() ?? "",
                ShirtNumber = (int)numShirtNumber.Value,
                Status = dbStatus // Записва се на английски в SQL
            };
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Моля, въведете Име и Фамилия на играча!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cboPosition.SelectedIndex == -1 || cboClub.SelectedValue == null || cboStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Моля, направете избор от всички падащи менюта!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void ClearInputs()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            numShirtNumber.Value = 1;
            dtpBirthDate.Value = DateTime.Now;
            cboPosition.SelectedIndex = -1;
            cboClub.SelectedIndex = -1;
            cboStatus.SelectedIndex = -1;
            _selectedPlayerId = -1;
            dgvPlayers.ClearSelection();
        }

        private void btnClear_Click(object sender, EventArgs e) => ClearInputs();
    }
}