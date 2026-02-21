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
        private void LoadClubs()                         // LOAD
        {
            try
            {
                dgvClubs.DataSource = repo.GetAll();

                Font myFont = new Font("Arial", 11);
                dgvClubs.DefaultCellStyle.Font = myFont;
                dgvClubs.AlternatingRowsDefaultCellStyle.Font = myFont;
                dgvClubs.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 11, FontStyle.Bold);

                if (dgvClubs.Columns["id"] != null) dgvClubs.Columns["id"].HeaderText = "ID";

                if (dgvClubs.Columns["name"] != null) dgvClubs.Columns["name"].HeaderText = "Име на отбор";

                if (dgvClubs.Columns["city"] != null) dgvClubs.Columns["city"].HeaderText = "Град";

                if (dgvClubs.Columns["created_at"] != null) dgvClubs.Columns["created_at"].HeaderText = "Създаден на";

                if (dgvClubs.Columns["id"] != null)
                {
                    dgvClubs.Columns["id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None; // Спира автоматичното разтягане
                    dgvClubs.Columns["id"].Width = 55; 
                    dgvClubs.Columns["id"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                //  Общи настройки на таблицата
                dgvClubs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvClubs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvClubs.AllowUserToAddRows = false;
                dgvClubs.RowTemplate.Height = 30;

                dgvClubs.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка: " + ex.Message);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)          // ADD BUTTON
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Моля, въведете име на клуба!", "Валидация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                repo.Add(txtName.Text.Trim(), txtCity.Text.Trim());
                MessageBox.Show("Клубът е добавен успешно!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadClubs();
                ClearInputs();
            }
            catch (Exception ex)
            {
                // Проверка за дублирано име 
                if (ex.Message.Contains("Duplicate entry"))
                {
                    MessageBox.Show($"Клуб с име '{txtName.Text}' вече съществува!", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Възникна грешка: " + ex.Message);
                }
            }
        }

        private void dgvClubs_CellClick(object sender, DataGridViewCellEventArgs e)           // CLUBS 
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvClubs.Rows[e.RowIndex]; 

                selectedId = Convert.ToInt32(row.Cells["id"].Value);

                txtName.Text = row.Cells["name"].Value.ToString();
                txtCity.Text = row.Cells["city"].Value.ToString();

            }
        }
        private void btnEdit_Click(object sender, EventArgs e)                         // EDIT/IPDATE BUTTON
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Моля, първо изберете клуб от таблицата!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                repo.Update(selectedId, txtName.Text.Trim(), txtCity.Text.Trim());
                MessageBox.Show("Данните са обновени!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadClubs();
                ClearInputs();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Duplicate entry"))
                {
                    MessageBox.Show($"Не може да преименувате клуба на '{txtName.Text}', защото това име вече е заето!", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Възникна грешка при редакцията: " + ex.Message);
                }
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
        private void btnClear_Click(object sender, EventArgs e)                // CLEAR BUTTON
        {
            ClearInputs();
        }
        private void ClearInputs()
        {
            txtName.Clear();
            txtCity.Clear();

            selectedId = -1;
            dgvClubs.ClearSelection();

            txtName.Focus();
        }
    }
}