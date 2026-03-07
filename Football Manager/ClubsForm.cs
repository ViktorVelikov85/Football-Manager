using System;
using System.Data;
using System.Windows.Forms;
using System.Xml.Linq;

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

        private void ClubsForm_Shown(object sender, EventArgs e)
        {
            ClearInputs();
        }
        private void LoadClubs()
        {
            try
            {
                dgvClubs.DataSource = repo.GetAll();
                // 1. Създаваме един общ шрифт
                Font commonFont = new Font("Arial", 12);

                dgvClubs.DefaultCellStyle.Font = commonFont;
                dgvClubs.AlternatingRowsDefaultCellStyle.Font = commonFont;
                dgvClubs.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 11, FontStyle.Bold);

                if (dgvClubs.Columns["id"] != null)
                {
                    dgvClubs.Columns["id"].HeaderText = "ID";
                    dgvClubs.Columns["id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dgvClubs.Columns["id"].Width = 40;
                    dgvClubs.Columns["id"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                // Година колона
                if (dgvClubs.Columns["founded_year"] != null)
                {
                    dgvClubs.Columns["founded_year"].HeaderText = "Година на създаване";
                    dgvClubs.Columns["founded_year"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dgvClubs.Columns["founded_year"].Width = 100;
                    dgvClubs.Columns["founded_year"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                // Другите колони остават на Fill, за да запълнят останалото място
                if (dgvClubs.Columns["name"] != null) dgvClubs.Columns["name"].HeaderText = "Име на отбор";
                if (dgvClubs.Columns["city"] != null) dgvClubs.Columns["city"].HeaderText = "Град";
                if (dgvClubs.Columns["stadium"] != null) dgvClubs.Columns["stadium"].HeaderText = "Стадион";

                dgvClubs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при зареждане: " + ex.Message);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Валидация
                if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtCity.Text))
                {
                    MessageBox.Show("Моля, попълнете Име и Град!");
                    return;
                }

                repo.Add(txtName.Text.Trim(), txtCity.Text.Trim(), txtStadium.Text.Trim(), txtCreatedIn.Text.Trim());

                MessageBox.Show("Клубът е добавен успешно!");
                LoadClubs(); // Обновяваме таблицата
                ClearInputs();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Duplicate entry"))
                    MessageBox.Show("Вече съществува клуб с това име!");
                else
                    MessageBox.Show("Грешка: " + ex.Message);
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
                txtStadium.Text = row.Cells["stadium"].Value?.ToString(); // Използваме ?, в случай че е празно
                txtCreatedIn.Text = row.Cells["founded_year"].Value?.ToString();
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Моля, изберете клуб от таблицата!");
                return;
            }

            try
            {
                repo.Update(selectedId, txtName.Text.Trim(), txtCity.Text.Trim(), txtStadium.Text.Trim(), txtCreatedIn.Text.Trim());

                MessageBox.Show("Данните бяха обновени!");
                LoadClubs();
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при обновяване: " + ex.Message);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)                       // DELETE BUTTON
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Изберете клуб за изтриване!", "Внимание");
                return;
            }

            DialogResult result = MessageBox.Show($"Сигурни ли сте, че искате да изтриете клуб '{txtName.Text}'?",
                "Потвърждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                repo.Delete(selectedId);
                LoadClubs();
                ClearInputs();
            }
        }
        private void ClearInputs()
        {
            txtName.Clear();
            txtCity.Clear();
            txtStadium.Clear();
            txtCreatedIn.Clear();
            selectedId = -1; // Нулираме избора
            dgvClubs.ClearSelection();

            // Казваме на таблицата, че няма активна клетка
            if (dgvClubs.CurrentCell != null)
            {
                dgvClubs.CurrentCell = null;
            }
            txtName.Focus();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void btnOpenPlayers_Click(object sender, EventArgs e)
        {
            this.Hide();
            PlayersForm playersForm = new PlayersForm();
            playersForm.ShowDialog();

            this.Show();
            ClearInputs(); 
        }
        private void ClubsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Този ред гарантира, че целият процес спира, когато затвориш главния прозорец
            Application.Exit();
        }

        
    }
}