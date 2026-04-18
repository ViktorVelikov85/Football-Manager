using Football_Manager.BLL;
using Football_Manager.Models;
using System.Data;

namespace Football_Manager.UI
{
    public partial class ClubsForm : Form
    {
        private readonly ClubService _service = new ClubService();
        private int _selectedId = -1;

        public ClubsForm()
        {
            InitializeComponent();
        }

        private void ClubsForm_Load(object sender, EventArgs e)
        {
            LoadClubs();
            ClearInputs();
        }

        private void ClubsForm_Shown(object sender, EventArgs e)
        {
            // Фокусираме името, когато формата се визуализира напълно
            if (txtName != null) txtName.Focus();
        }

        private void LoadClubs()
        {
            try
            {
                if (dgvClubs == null) return;

                DataTable dt = _service.GetAllClubs();
                dgvClubs.DataSource = dt;
                SetGridStyle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при зареждане на клубове: " + ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Моля, въведете име на отбор!");
                return;
            }

            var club = MapInputsToModel();
            if (_service.SaveClub(club, true, out string message))
            {
                FinishOperation(message);
            }
            else
            {
                MessageBox.Show(message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (_selectedId == -1) { MessageBox.Show("Моля, изберете клуб от списъка!"); return; }

            var club = MapInputsToModel();
            club.Id = _selectedId;

            if (_service.SaveClub(club, false, out string message))
            {
                FinishOperation(message);
            }
            else
            {
                MessageBox.Show(message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedId == -1) { MessageBox.Show("Моля, изберете клуб за изтриване!"); return; }

            if (MessageBox.Show($"Сигурни ли сте, че искате да изтриете '{txtName.Text}'?", "Потвърждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    _service.DeleteClub(_selectedId);
                    FinishOperation("Клубът е изтрит успешно!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Грешка при изтриване: " + ex.Message);
                }
            }
        }

        private Club MapInputsToModel()
        {
            return new Club
            {
                Name = txtName.Text.Trim(),
                City = txtCity.Text.Trim(),
                Stadium = txtStadium.Text.Trim(),
                FoundedYear = txtCreatedIn.Text.Trim()
            };
        }

        private void FinishOperation(string message)
        {
            MessageBox.Show(message, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadClubs();
            ClearInputs();
        }

        private void ClearInputs()
        {
            if (txtName != null) txtName.Clear();
            if (txtCity != null) txtCity.Clear();
            if (txtStadium != null) txtStadium.Clear();
            if (txtCreatedIn != null) txtCreatedIn.Clear();

            _selectedId = -1;

            if (dgvClubs != null)
            {
                dgvClubs.ClearSelection();
                if (dgvClubs.CurrentCell != null) dgvClubs.CurrentCell = null;
            }

            if (txtName != null) txtName.Focus();
        }

        private void dgvClubs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                DataGridViewRow row = dgvClubs.Rows[e.RowIndex];

                // Проверка дали колоните съществуват, за да се избегне NullReferenceException
                if (row.Cells["id"].Value != null)
                {
                    _selectedId = Convert.ToInt32(row.Cells["id"].Value);
                    txtName.Text = row.Cells["name"].Value?.ToString() ?? "";
                    txtCity.Text = row.Cells["city"].Value?.ToString() ?? "";
                    txtStadium.Text = row.Cells["stadium"].Value?.ToString() ?? "";
                    txtCreatedIn.Text = row.Cells["founded_year"].Value?.ToString() ?? "";
                }
            }
            catch (Exception ex)
            {
                // Логваме грешката, но не спираме приложението
                Console.WriteLine("Грешка при избор на ред: " + ex.Message);
            }
        }

        private void SetGridStyle()
        {
            if (dgvClubs == null || dgvClubs.DataSource == null) return;

            Font customFont = new Font("Arial", 12);
            dgvClubs.DefaultCellStyle.Font = customFont;
            dgvClubs.AlternatingRowsDefaultCellStyle.Font = customFont;

            dgvClubs.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);

            dgvClubs.RowHeadersVisible = false;
            dgvClubs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClubs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvClubs.RowTemplate.Height = 30;


            var columnNames = new Dictionary<string, string>
            {
                { "id", "ID" },
                { "name", "Име на отбор" },
                { "city", "Град" },
                { "stadium", "Стадион" },
                { "founded_year", "Година" }
            };

            foreach (var pair in columnNames)
            {
                if (dgvClubs.Columns.Contains(pair.Key))
                {
                    dgvClubs.Columns[pair.Key].HeaderText = pair.Value;
                }
            }

            if (dgvClubs.Columns.Contains("id"))
            {
                dgvClubs.Columns["id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgvClubs.Columns["id"].Width = 60; 
            }
        }
        private void btnClear_Click(object sender, EventArgs e) => ClearInputs();
    }
}