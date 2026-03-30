using Football_Manager;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Football_Manager
{
    public partial class TransfersHistoryForm : Form
    {
        private TransfersRepository transferRepo = new TransfersRepository();

        public TransfersHistoryForm()
        {
            InitializeComponent();
        }

        private void TransfersHistoryForm_Load(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            try
            {
                // Използваме същото име на текстовото поле за търсене
                dgvTransfers.DataSource = transferRepo.GetTransfers(txtSearchNameTransfer.Text.Trim());
                SetGridStyle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при зареждане на историята: " + ex.Message);
            }
        }

        private void SetGridStyle()
        {
            if (dgvTransfers.DataSource == null) return;

            // Идентични настройки като в TransfersForm
            dgvTransfers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTransfers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTransfers.AllowUserToAddRows = false;
            dgvTransfers.ReadOnly = true;
            dgvTransfers.RowHeadersVisible = false;

            Font commonFont = new Font("Arial", 12);
            dgvTransfers.DefaultCellStyle.Font = commonFont;
            dgvTransfers.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);

            dgvTransfers.ClearSelection();

            // Форматиране на колоните (съвпадащи с изгледа в TransfersForm)
            if (dgvTransfers.Columns["Такса"] != null)
            {
                dgvTransfers.Columns["Такса"].HeaderText = "Сума (€)";
                dgvTransfers.Columns["Такса"].DefaultCellStyle.Format = "N2";
                dgvTransfers.Columns["Такса"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (dgvTransfers.Columns["Дата"] != null)
            {
                dgvTransfers.Columns["Дата"].DefaultCellStyle.Format = "dd.MM.yyyy";
            }
        }

        // Събитие за търсене в реално време
        private void txtSearchNameTransfers_TextChanged(object sender, EventArgs e)
        {
            RefreshGrid();
        }
    }
}