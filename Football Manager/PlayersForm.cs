using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Football_Manager
{
    public partial class PlayersForm : Form
    {
        PlayersRepository repo = new PlayersRepository();
        int selectedPlayerId = -1;

        public PlayersForm()
        {
            InitializeComponent();
        }

        private void PlayersForm_Load(object sender, EventArgs e)
        {
            // 1. Пълнене на позициите (за Добавяне/Редакция)
            cboPosition.Items.Clear();
            cboPosition.Items.AddRange(new string[] { "GK", "DF", "MF", "FW" });

            // 2. Пълнене на статуса (за Добавяне/Редакция)
            cboStatus.Items.Clear();
            cboStatus.Items.AddRange(new string[] { "Active", "Injured", "Suspended" });
            cboStatus.SelectedIndex = 0;

            // 3. Пълнене на позициите за ФИЛТЪРА (с опция "Всички")
            cboFilterPosition.Items.Clear();
            cboFilterPosition.Items.AddRange(new string[] { "Всички", "GK", "DF", "MF", "FW" });
            cboFilterPosition.SelectedIndex = 0;

            // 4. Зареждане на клубовете от базата данни
            DataTable clubs = Db.GetTable("SELECT id, name FROM clubs");

            // --- Настройка на cboClub (за Добавяне) ---
            cboClub.DisplayMember = "name";
            cboClub.ValueMember = "id";
            cboClub.DataSource = clubs; // DataSource винаги последен
            cboClub.SelectedIndex = -1; // Да започне празно

            // --- Настройка на cboFilterClub (за Филтриране) ---
            // Правим копие на таблицата, за да добавим "Всички отбори" без да развалим първия комбо бокс
            DataTable filterClubs = clubs.Copy();
            DataRow row = filterClubs.NewRow();
            row["id"] = 0;
            row["name"] = "Всички";
            filterClubs.Rows.InsertAt(row, 0);

            cboFilterClub.DisplayMember = "name";
            cboFilterClub.ValueMember = "id";
            cboFilterClub.DataSource = filterClubs;
            cboFilterClub.SelectedIndex = 0; // По подразбиране "Всички отбори"

            // 5. Зареждане на играчите в таблицата
            LoadPlayers();

            // 6. Пълно изчистване на селекцията и полетата при старт
            ClearInputs();
        }
        private void ApplyFilters()
        {
            int? selectedClubId = null;
            if (cboFilterClub.SelectedValue is int cid && cid > 0) selectedClubId = cid;

            string selectedPosition = cboFilterPosition.SelectedItem?.ToString() ?? "Всички";
            string searchText = txtSearchName.Text.Trim();

            dgvPlayers.DataSource = repo.GetFilteredPlayers(selectedClubId, selectedPosition, searchText);
            SetupGridHeaders(); // Прилагаме настройките отново
        }
        private void btnClearFilters_Click(object sender, EventArgs e)
        {
            txtSearchName.Clear();

            // Връщаме филтъра за отбор на "Всички" (който е на индекс 0)
            if (cboFilterClub.Items.Count > 0)
            {
                cboFilterClub.SelectedIndex = 0;
            }

            // Връщаме филтъра за позиция на "Всички" (който е на индекс 0)
            if (cboFilterPosition.Items.Count > 0)
            {
                cboFilterPosition.SelectedIndex = 0;
            }
            ApplyFilters();
            txtSearchName.Focus();
        }
        private void SetupGridHeaders()
        {
            if (dgvPlayers.Columns.Count == 0) return;

            // --- 1. Настройка на шрифта (за да не се нулира) ---
            Font commonFont = new Font("Arial", 10);
            dgvPlayers.DefaultCellStyle.Font = commonFont;
            dgvPlayers.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dgvPlayers.RowsDefaultCellStyle.Font = commonFont;
            dgvPlayers.AlternatingRowsDefaultCellStyle.Font = commonFont;

            // --- 2. Преименуване на български ---
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
                {
                    dgvPlayers.Columns[header.Key].HeaderText = header.Value;
                }
            }

            // --- 3. Настройка на размерите ---
            if (dgvPlayers.Columns.Contains("id"))
            {
                dgvPlayers.Columns["id"].Width = 45;
                dgvPlayers.Columns["id"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvPlayers.Columns.Contains("shirt_number"))
            {
                dgvPlayers.Columns["shirt_number"].Width = 45;
                dgvPlayers.Columns["shirt_number"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            dgvPlayers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void cboFilterClub_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void cboFilterPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void LoadPlayers()
        {
            dgvPlayers.DataSource = repo.GetPlayers();
            SetupGridHeaders(); // Всички заглавия и размери се поемат оттук
        }

        //  зареждане на данните при клик в таблицата
        private void dgvPlayers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPlayers.Rows[e.RowIndex];
                selectedPlayerId = Convert.ToInt32(row.Cells["id"].Value);

                // Разделяме FullName на First и Last Name за двете полета
                string fullName = row.Cells["full_name"].Value.ToString();
                string[] nameParts = fullName.Split(new[] { ' ' }, 2);

                txtFirstName.Text = nameParts[0];
                txtLastName.Text = nameParts.Length > 1 ? nameParts[1] : "";

                // Зареждаме останалите данни
                cboPosition.SelectedItem = row.Cells["position"].Value.ToString();
                numShirtNumber.Value = Convert.ToInt32(row.Cells["shirt_number"].Value);
                dtpBirthDate.Value = Convert.ToDateTime(row.Cells["birth_date"].Value);
                cboClub.Text = row.Cells["club_name"].Value.ToString();

                // Проверяваме дали колоната status съществува в SELECT-а на репозиторито
                if (dgvPlayers.Columns.Contains("status"))
                    cboStatus.SelectedItem = row.Cells["status"].Value.ToString();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // 1. Проверка за празни имена или наличие на цифри
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Моля, попълнете и двете имена!");
                return;
            }
            if (txtFirstName.Text.Any(char.IsDigit) || txtLastName.Text.Any(char.IsDigit))
            {
                MessageBox.Show("Имената не трябва да съдържат цифри!");
                return;
            }

            // 2. Проверка за избран клуб
            if (cboClub.SelectedValue == null)
            {
                MessageBox.Show("Моля, изберете клуб от списъка!");
                return;
            }

            // 3. Проверка за позиция и статус
            if (cboPosition.SelectedIndex == -1 || cboStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Моля, изберете позиция и статус!");
                return;
            }

            // 4. Проверка на възрастта 
            int age = DateTime.Now.Year - dtpBirthDate.Value.Year;
            if (dtpBirthDate.Value > DateTime.Now || age < 15 || age > 60)
            {
                MessageBox.Show("Невалидна дата на раждане! Възрастта трябва да е между 15 и 60 години.");
                return;
            }

            // --- АКО ВСИЧКО Е НАРЕД, ПРОДЪЛЖАВАМЕ КЪМ ЗАПИС ---

            try
            {
                string fullName = $"{txtFirstName.Text.Trim()} {txtLastName.Text.Trim()}";

                repo.Add(
                    Convert.ToInt32(cboClub.SelectedValue),
                    fullName,
                    dtpBirthDate.Value.ToString("yyyy-MM-dd"),
                    cboPosition.SelectedItem.ToString(),
                    (int)numShirtNumber.Value,
                    cboStatus.SelectedItem.ToString()
                );

                MessageBox.Show("Играчът е добавен успешно!");
                LoadPlayers();
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при запис в базата: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedPlayerId == -1) return;

            string fullName = $"{txtFirstName.Text.Trim()} {txtLastName.Text.Trim()}";
            repo.Update(selectedPlayerId, Convert.ToInt32(cboClub.SelectedValue), fullName, dtpBirthDate.Value.ToString("yyyy-MM-dd"),
                        cboPosition.SelectedItem.ToString(), (int)numShirtNumber.Value, cboStatus.SelectedItem.ToString());

            LoadPlayers();
            ClearInputs();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedPlayerId == -1) return;
            repo.Delete(selectedPlayerId);
            LoadPlayers();
            ClearInputs();
        }

        private void btnClear_Click(object sender, EventArgs e) => ClearInputs();

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void ClearInputs()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            numShirtNumber.Value = 1;
            dtpBirthDate.Value = DateTime.Now;
            cboPosition.SelectedIndex = -1;
            cboClub.SelectedIndex = -1;
            cboStatus.SelectedIndex = 0;

            dgvPlayers.ClearSelection();

            // Казваме на таблицата, че няма активна клетка
            if (dgvPlayers.CurrentCell != null)
            {
                dgvPlayers.CurrentCell = null;
            }

            selectedPlayerId = -1;
            txtFirstName.Focus();
        }

        private void txtSearchName_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        
    }
}