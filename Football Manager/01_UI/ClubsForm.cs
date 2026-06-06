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

        // Жизнен цикъл на формата
        private void ClubsForm_Load(object sender, EventArgs e)
        {
            // Всички стилове и колони на таблицата са настроени изцяло в Properties
            dgvClubs.AutoGenerateColumns = false;

            LoadClubs();
            ClearInputs();
        }

        private void ClubsForm_Shown(object sender, EventArgs e)
        {
            if (txtName != null) txtName.Focus();
        }

        // Зареждане на данни
        private void LoadClubs()
        {
            try
            {
                if (dgvClubs == null) return;

                DataTable dt = _service.GetAllClubs();
                dgvClubs.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при зареждане на клубове: " + ex.Message);
            }
        }

        // Операции с данни (CRUD)
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
            if (_selectedId == -1)
            {
                MessageBox.Show("Моля, изберете клуб от списъка!");
                return;
            }

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
            if (_selectedId == -1)
            {
                MessageBox.Show("Моля, изберете клуб от таблицата за изтриване!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string clubName = dgvClubs.CurrentRow?.Cells["colName"].Value?.ToString() ?? "този отбор";

            string confirmMessage = $"ВНИМАНИЕ!\n\nСигурни ли сте, че искате да изтриете отбор '{clubName}'?\n\n" +
                                    "Това действие е необратимо и ЩЕ ИЗТРИЕ ОЩЕ:\n" +
                                    "• Всички мачове, в които отборът участва (домакин или гост)\n" +
                                    "• Всички футболисти, играещи в този отбор\n" +
                                    "• Статистиката за голове, картони и фалове от тези мачове";

            DialogResult result = MessageBox.Show(confirmMessage, "Потвърждение за ИЗТРИВАНЕ",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Изключително чисто извикване – UI просто казва НАЙ-ОТГОРЕ какво иска, 
                    // а долните слоеве знаят КАК да го направят в базата данни.
                    _service.DeleteClub(_selectedId);

                    FinishOperation("Клубът и всички свързани с него мачове и играчи бяха изтрити успешно!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Грешка при изтриване: " + ex.Message, "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Събития на таблицата
        private void dgvClubs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvClubs.Rows[e.RowIndex];
            var dataRow = (DataRowView)row.DataBoundItem;

            // Извличаме оригиналното ID от DataTable структурата в базата
            _selectedId = Convert.ToInt32(dataRow["id"]);

            // Попълване на полетата при избор на ред
            txtName.Text = row.Cells["colName"].Value?.ToString() ?? "";
            txtCity.Text = row.Cells["colCity"].Value?.ToString() ?? "";
            txtStadium.Text = row.Cells["colStadium"].Value?.ToString() ?? "";
            txtCreatedIn.Text = row.Cells["colFoundedYear"].Value?.ToString() ?? "";
        }

        // Помощни методи
        private Club MapInputsToModel()
        {
            // Преобразуване на текстовите полета в бизнес модел
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
            // Общ метод за опресняване след успешна трансакция
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

            // Нулиране на фокуса и маркираните редове в таблицата
            if (dgvClubs != null)
            {
                dgvClubs.ClearSelection();
                if (dgvClubs.CurrentCell != null) dgvClubs.CurrentCell = null;
            }

            if (txtName != null) txtName.Focus();
        }

        private void btnClear_Click(object sender, EventArgs e) => ClearInputs();
    }
}