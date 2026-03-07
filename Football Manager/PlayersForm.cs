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
            // Пълним комбо боксовете
            DataTable clubs = Db.GetTable("SELECT id, name FROM clubs");
            cboClub.DataSource = clubs;
            cboClub.DisplayMember = "name";
            cboClub.ValueMember = "id";

            cboPosition.Items.AddRange(new string[] { "GK", "DF", "MF", "FW" });
            cboStatus.Items.AddRange(new string[] { "Active", "Injured", "Suspended" });
            cboStatus.SelectedIndex = 0;

            LoadPlayers();
            ClearInputs();
        }

        private void LoadPlayers()
        {
            // 1. Зареждане на данните
            dgvPlayers.DataSource = repo.GetPlayers();

            // 2. Настройка на шрифта
            Font commonFont = new Font("Arial", 10);
            dgvPlayers.DefaultCellStyle.Font = commonFont;
            dgvPlayers.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dgvPlayers.RowsDefaultCellStyle.Font = commonFont;


            // 3. Преименуване на колоните на български
            if (dgvPlayers.Columns["id"] != null) dgvPlayers.Columns["id"].HeaderText = "ID";
            if (dgvPlayers.Columns["full_name"] != null) dgvPlayers.Columns["full_name"].HeaderText = "Име на играч";
            if (dgvPlayers.Columns["club_name"] != null) dgvPlayers.Columns["club_name"].HeaderText = "Отбор";
            if (dgvPlayers.Columns["position"] != null) dgvPlayers.Columns["position"].HeaderText = "Позиция";
            if (dgvPlayers.Columns["shirt_number"] != null) dgvPlayers.Columns["shirt_number"].HeaderText = "№";
            if (dgvPlayers.Columns["birth_date"] != null) dgvPlayers.Columns["birth_date"].HeaderText = "Дата на раждане";
            if (dgvPlayers.Columns["status"] != null) dgvPlayers.Columns["status"].HeaderText = "Статус";

            dgvPlayers.Columns["birth_date"].DefaultCellStyle.Format = "dd.MM.yyyy";
            // 4. Настройка на ширината (Размери)
            // Изключваме автоматичното разтягане за малките колони
            if (dgvPlayers.Columns["id"] != null)
            {
                dgvPlayers.Columns["id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgvPlayers.Columns["id"].Width = 40;
                dgvPlayers.Columns["id"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvPlayers.Columns["shirt_number"] != null)
            {
                dgvPlayers.Columns["shirt_number"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgvPlayers.Columns["shirt_number"].Width = 40;
                dgvPlayers.Columns["shirt_number"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            // Всички останали колони се разпределят равномерно
            dgvPlayers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
    }
}