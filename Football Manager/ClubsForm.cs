using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq; 
using System.Windows.Forms;

namespace Football_Manager
{
    public partial class ClubsForm : Form
    {
        ClubsRepository repo = new ClubsRepository();
        int selectedId = -1;

        public ClubsForm()
        {
            InitializeComponent();
            LoadClubs();
        }

        private void ClubsForm_Shown(object sender, EventArgs e) => ClearInputs();

        private void LoadClubs()
        {
            try
            {
                dgvClubs.DataSource = repo.GetAll();
                SetGridStyle(); // Изнасяме стилизирането в отделен метод (DRY)
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при зареждане: " + ex.Message);
            }
        }

        private void SetGridStyle()
        {
            // Проверка: ако няма източник на данни, не прави нищо
            if (dgvClubs.DataSource == null) return;

            // Общи настройки
            dgvClubs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClubs.RowHeadersVisible = false;
            dgvClubs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            Font commonFont = new Font("Arial", 12);
            dgvClubs.DefaultCellStyle.Font = commonFont;
            dgvClubs.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 11, FontStyle.Bold);

            // Речник със заглавията
            var columnNames = new Dictionary<string, string>
            {
                { "id", "ID" },
                { "name", "Име на отбор" },
                { "city", "Град" },
                { "stadium", "Стадион" },
                { "founded_year", "Година на създаване" }
            };

            foreach (var pair in columnNames)
            {
                // Проверяваме дали колоната съществува в Grid-а
                if (dgvClubs.Columns.Contains(pair.Key))
                {
                    dgvClubs.Columns[pair.Key].HeaderText = pair.Value;
                }
            }

            if (dgvClubs.Columns.Contains("id"))
            {
                dgvClubs.Columns["id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgvClubs.Columns["id"].Width = 45;
            }

            if (dgvClubs.Columns.Contains("founded_year"))
            {
                dgvClubs.Columns["founded_year"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgvClubs.Columns["founded_year"].Width = 100;
            }
        }

        private void dgvClubs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvClubs.Rows[e.RowIndex];

                selectedId = Convert.ToInt32(row.Cells["id"].Value);
                txtName.Text = row.Cells["name"].Value.ToString();
                txtCity.Text = row.Cells["city"].Value.ToString();

                // Използваме null-conditional (?.) - ако клетката е празна, връща null
                txtStadium.Text = row.Cells["stadium"].Value?.ToString();
                txtCreatedIn.Text = row.Cells["founded_year"].Value?.ToString();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            try
            {
                repo.Add(txtName.Text.Trim(), txtCity.Text.Trim(), txtStadium.Text.Trim(), txtCreatedIn.Text.Trim());
                FinishOperation("Клубът е добавен успешно!");
            }
            catch (Exception ex)
            {
                // Проверка за дублиране на име чрез съобщението от MySQL
                string msg = ex.Message.Contains("Duplicate entry") ? "Вече съществува такъв клуб!" : ex.Message;
                MessageBox.Show(msg);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selectedId == -1) { MessageBox.Show("Изберете клуб!"); return; }
            if (!ValidateInputs()) return;

            try
            {
                repo.Update(selectedId, txtName.Text.Trim(), txtCity.Text.Trim(), txtStadium.Text.Trim(), txtCreatedIn.Text.Trim());
                FinishOperation("Данните бяха обновени!");
            }
            catch (Exception ex) { MessageBox.Show("Грешка: " + ex.Message); }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedId == -1) return;

            if (MessageBox.Show($"Изтриване на '{txtName.Text}'?", "Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                repo.Delete(selectedId);
                FinishOperation("Изтрито успешно!");
            }
        }

        // --- Помощни методи ---

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtCity.Text))
            {
                MessageBox.Show("Моля, попълнете Име и Град!");
                return false;
            }
            return true;
        }

        private void FinishOperation(string message)
        {
            MessageBox.Show(message);
            LoadClubs();   // Обновява таблицата
            ClearInputs(); // Чисти полетата
        }

        private void ClearInputs()
        {
            // LINQ: Намира всички TextBox-ове във формата и ги изчиства
            this.Controls.OfType<TextBox>().ToList().ForEach(t => t.Clear());

            selectedId = -1;
            dgvClubs.ClearSelection();
            if (dgvClubs.CurrentCell != null) dgvClubs.CurrentCell = null;
            txtName.Focus();
        }

        private void btnClear_Click(object sender, EventArgs e) => ClearInputs();

        private void btnOpenPlayers_Click(object sender, EventArgs e)
        {
            this.Hide();
            new PlayersForm().ShowDialog();
            this.Show();
            LoadClubs(); // Обновяваме в случай на промени
        }
    }
}