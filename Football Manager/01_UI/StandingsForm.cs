using Football_Manager.BLL;
using Football_Manager.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Football_Manager.UI
{
    public partial class StandingsForm : Form
    {
        private readonly LeaguesService _leaguesService = new LeaguesService();
        private readonly StandingsService _standingsService = new StandingsService();

        public StandingsForm()
        {
            InitializeComponent();
        }

        private void StandingsForm_Load(object sender, EventArgs e)
        {
            this.Text = "Класиране на отборите";

            // Настройка на шрифтовете за таблицата с класирането
            Font arial12 = new Font("Arial", 12);
            Font arial12Bold = new Font("Arial", 12, FontStyle.Bold);
            dgvStandings.DefaultCellStyle.Font = arial12;
            dgvStandings.ColumnHeadersDefaultCellStyle.Font = arial12Bold;
            dgvStandings.RowTemplate.Height = 35;

            LoadLeaguesComboBox();
        }

        // Зарежда лигите в падащото меню
        private void LoadLeaguesComboBox()
        {
            try
            {
                // Използваме твоя съществуващ LeaguesService
                cboLeagues.DataSource = _leaguesService.GetLeagues();
                cboLeagues.DisplayMember = "name"; // Или каквото е името на колоната в твоята база (напр. "name")
                cboLeagues.ValueMember = "id";

                // Закачаме събитието за смяна на лига чак СЛЕД като сме заредили данните, за да няма грешки
                cboLeagues.SelectedIndexChanged += cboLeagues_SelectedIndexChanged;

                // Зареждаме класирането за първата избрана лига веднага
                LoadStandings();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при зареждане на лигите: " + ex.Message);
            }
        }

        // Изчислява и визуализира класирането
        private void LoadStandings()
        {
            if (cboLeagues.SelectedValue == null) return;

            try
            {
                int leagueId = Convert.ToInt32(cboLeagues.SelectedValue);

                // Извикваме тежката логика от BLL слоя
                List<Standing> standings = _standingsService.GetStandings(leagueId);

                // Закачаме списъка към таблицата
                dgvStandings.DataSource = standings;

                // --- КРАСИВО ОФОРМЛЕНИЕ И СКРИВАНЕ НА ИЗЛИШНИТЕ КОЛОНИ ---
                var cols = dgvStandings.Columns;

                // Скриваме колоните, които не трябва да се виждат директно
                if (cols.Contains("ClubId")) cols["ClubId"].Visible = false;
                if (cols.Contains("GoalsFor")) cols["GoalsFor"].Visible = false;
                if (cols.Contains("GoalsAgainst")) cols["GoalsAgainst"].Visible = false;

                // Преименуваме останалите колони на хубав български език
                if (cols.Contains("ClubName")) cols["ClubName"].HeaderText = "Отбор";
                if (cols.Contains("MatchesPlayed")) cols["MatchesPlayed"].HeaderText = "Мачове";
                if (cols.Contains("Wins")) cols["Wins"].HeaderText = "Победи";
                if (cols.Contains("Draws")) cols["Draws"].HeaderText = "Равни";
                if (cols.Contains("Losses")) cols["Losses"].HeaderText = "Загуби";
                if (cols.Contains("GoalsDisplay")) cols["GoalsDisplay"].HeaderText = "Голове";
                if (cols.Contains("GoalDifference")) cols["GoalDifference"].HeaderText = "Голова разлика";
                if (cols.Contains("Points")) cols["Points"].HeaderText = "Точки";

                // Даваме малко повече тежест на колоната за името на отбора
                if (cols.Contains("ClubName")) cols["ClubName"].FillWeight = 180;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при изчисляване на класирането: " + ex.Message);
            }
        }

        // Когато потребителят избере друга лига от менюто
        private void cboLeagues_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStandings();
        }

        // Бутонът за ръчно обновяване
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadStandings();
            MessageBox.Show("Класирането беше обновено успешно!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}