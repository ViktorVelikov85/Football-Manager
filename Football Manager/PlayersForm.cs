using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Football_Manager
{
    public partial class PlayersForm : Form
    {
        private readonly PlayersRepository _repo = new PlayersRepository();
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
        }

        private void SetupComboBoxes()
        {
            // 1. Позиции и Статус (Използваме масиви за по-чист код)
            cboPosition.Items.Clear();
            cboPosition.Items.AddRange(new string[] { "GK", "DF", "MF", "FW" });

            cboStatus.Items.Clear();
            cboStatus.Items.AddRange(new string[] { "Active", "Injured", "Suspended" });
            cboStatus.SelectedIndex = 0;

            // 2. Филтър Позиции
            cboFilterPosition.Items.Clear();
            cboFilterPosition.Items.AddRange(new string[] { "Всички", "GK", "DF", "MF", "FW" });
            cboFilterPosition.SelectedIndex = 0;

            // 3. Зареждане на клубове (с DRY подход за двете комбо кутии)
            try
            {
                DataTable clubs = Db.GetTable("SELECT id, name FROM clubs");

                // Записване (cboClub)
                cboClub.DisplayMember = "name";
                cboClub.ValueMember = "id";
                cboClub.DataSource = clubs;
                cboClub.SelectedIndex = -1;

                // Филтриране (cboFilterClub)
                DataTable filterClubs = clubs.Copy();
                DataRow row = filterClubs.NewRow();
                row["id"] = 0;
                row["name"] = "Всички";
                filterClubs.Rows.InsertAt(row, 0);

                cboFilterClub.DisplayMember = "name";
                cboFilterClub.ValueMember = "id";
                cboFilterClub.DataSource = filterClubs;
                cboFilterClub.SelectedIndex = 0;
            }
            catch (Exception ex) { MessageBox.Show("Грешка при клубове: " + ex.Message); }
        }

        private void LoadPlayers()
        {
            try
            {
                dgvPlayers.DataSource = _repo.GetPlayers();
                SetGridStyle();
            }
            catch (Exception ex) { MessageBox.Show("Грешка при зареждане: " + ex.Message); }
        }

        private void SetGridStyle()
        {
            if (dgvPlayers.DataSource == null) return;

            // Базови настройки (DRY - същите като в ClubsForm)
            dgvPlayers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPlayers.MultiSelect = false;
            dgvPlayers.AllowUserToAddRows = false;
            dgvPlayers.ReadOnly = true;
            dgvPlayers.RowHeadersVisible = false;
            dgvPlayers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            Font commonFont = new Font("Arial", 12);
            dgvPlayers.DefaultCellStyle.Font = commonFont;
            dgvPlayers.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);

            // Именуване на колони чрез Dictionary
            var headers = new Dictionary<string, string>
            {
                { "id", "ID" },
                { "full_name", "Име на играч" },
                { "club_name", "Отбор" },
                { "position", "Позиция" },
                { "shirt_number", "№" },
                { "birth_date", "Дата на раждане" },
                { "status", "Статус" }
            };

            foreach (var header in headers)
            {
                if (dgvPlayers.Columns.Contains(header.Key))
                    dgvPlayers.Columns[header.Key].HeaderText = header.Value;
            }

            // Настройка на ширини и тежести (Weights)
            if (dgvPlayers.Columns.Contains("club_id")) dgvPlayers.Columns["club_id"].Visible = false;
            if (dgvPlayers.Columns.Contains("id")) { dgvPlayers.Columns["id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None; dgvPlayers.Columns["id"].Width = 45; }
            if (dgvPlayers.Columns.Contains("full_name")) dgvPlayers.Columns["full_name"].FillWeight = 180;
            if (dgvPlayers.Columns.Contains("shirt_number")) { dgvPlayers.Columns["shirt_number"].Width = 50; dgvPlayers.Columns["shirt_number"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; }
        }

        private void ApplyFilters()
        {
            int? clubId = (cboFilterClub.SelectedValue is int cid && cid > 0) ? cid : (int?)null;
            string pos = cboFilterPosition.SelectedItem?.ToString() ?? "Всички";

            dgvPlayers.DataSource = _repo.GetFilteredPlayers(clubId, pos, txtSearchName.Text.Trim());
            SetGridStyle();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            ExecuteAction(() => {
                string fullName = $"{txtFirstName.Text.Trim()} {txtLastName.Text.Trim()}";
                _repo.Add(Convert.ToInt32(cboClub.SelectedValue), fullName, dtpBirthDate.Value.ToString("yyyy-MM-dd"),
                          cboPosition.SelectedItem.ToString(), (int)numShirtNumber.Value, cboStatus.SelectedItem.ToString());
                MessageBox.Show("Играчът е добавен!");
            });
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedPlayerId == -1 || !ValidateInputs()) return;

            ExecuteAction(() => {
                string fullName = $"{txtFirstName.Text.Trim()} {txtLastName.Text.Trim()}";
                _repo.Update(_selectedPlayerId, Convert.ToInt32(cboClub.SelectedValue), fullName, dtpBirthDate.Value.ToString("yyyy-MM-dd"),
                             cboPosition.SelectedItem.ToString(), (int)numShirtNumber.Value, cboStatus.SelectedItem.ToString());
                MessageBox.Show("Данните са обновени!");
            });
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedPlayerId == -1) return;

            if (MessageBox.Show("Сигурни ли сте?", "Изтриване", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ExecuteAction(() => _repo.Delete(_selectedPlayerId));
            }
        }

        private void dgvPlayers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dgvPlayers.Rows[e.RowIndex];

            _selectedPlayerId = Convert.ToInt32(row.Cells["id"].Value);

            // Разделяне на имената (Ламбда/LINQ логика за безопасност)
            string fullName = row.Cells["full_name"].Value?.ToString() ?? "";
            var parts = fullName.Split(new[] { ' ' }, 2);
            txtFirstName.Text = parts[0];
            txtLastName.Text = parts.Length > 1 ? parts[1] : "";

            cboPosition.SelectedItem = row.Cells["position"].Value?.ToString();
            numShirtNumber.Value = Convert.ToDecimal(row.Cells["shirt_number"].Value);
            cboStatus.SelectedItem = row.Cells["status"].Value?.ToString();

            if (row.Cells["birth_date"].Value != DBNull.Value)
                dtpBirthDate.Value = Convert.ToDateTime(row.Cells["birth_date"].Value);

            if (dgvPlayers.Columns.Contains("club_id"))
                cboClub.SelectedValue = row.Cells["club_id"].Value;
        }

        // --- Помощни методи ---

        private bool ValidateInputs()
        {
            // LINQ проверка за празни полета или цифри в имената
            if (new[] { txtFirstName, txtLastName }.Any(t => string.IsNullOrWhiteSpace(t.Text)))
            {
                MessageBox.Show("Попълнете имената!");
                return false;
            }

            if (txtFirstName.Text.Any(char.IsDigit) || txtLastName.Text.Any(char.IsDigit))
            {
                MessageBox.Show("Имената не трябва да съдържат цифри!");
                return false;
            }

            int age = DateTime.Now.Year - dtpBirthDate.Value.Year;
            if (age < 15 || age > 60)
            {
                MessageBox.Show("Възрастта трябва да е между 15 и 60 г.");
                return false;
            }

            return cboClub.SelectedValue != null;
        }

        private void ExecuteAction(Action action)
        {
            try
            {
                action();
                LoadPlayers();
                ClearInputs();
            }
            catch (Exception ex) { MessageBox.Show("Грешка: " + ex.Message); }
        }

        private void ClearInputs()
        {
            // Изчистване на всички TextBox контроли наведнъж
            this.Controls.OfType<TextBox>().ToList().ForEach(t => t.Clear());
            txtSearchName.Clear(); // Ако не е в основната колекция

            numShirtNumber.Value = 1;
            dtpBirthDate.Value = DateTime.Now;
            cboPosition.SelectedIndex = -1;
            cboClub.SelectedIndex = -1;
            cboStatus.SelectedIndex = 0;

            _selectedPlayerId = -1;
            dgvPlayers.ClearSelection();
            if (dgvPlayers.CurrentCell != null) dgvPlayers.CurrentCell = null;
        }

        // Събития за филтри
        private void txtSearchName_TextChanged(object sender, EventArgs e) => ApplyFilters();
        private void cboFilterClub_SelectedIndexChanged(object sender, EventArgs e) => ApplyFilters();
        private void cboFilterPosition_SelectedIndexChanged(object sender, EventArgs e) => ApplyFilters();
        private void btnClearFilters_Click(object sender, EventArgs e) { txtSearchName.Clear(); cboFilterClub.SelectedIndex = 0; cboFilterPosition.SelectedIndex = 0; ApplyFilters(); }
        private void btnClear_Click(object sender, EventArgs e) => ClearInputs();
    }
}