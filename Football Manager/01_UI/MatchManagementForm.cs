using Football_Manager.BLL;
using System;
using System.Data;
using System.Windows.Forms;

namespace Football_Manager.UI
{
    public partial class MatchManagementForm : Form
    {
        private readonly MatchesService _matchesService = new MatchesService();
        private readonly int _matchId, _homeTeamId, _awayTeamId;
        private readonly string _homeTeamName, _awayTeamName;
        private readonly DateTime _matchDate;

        // Пазим таблицата с играчите в паметта на формата, за да можем 
        // лесно да вземем id и club_id при клик на бутона
        private DataTable _playersTable;

        public MatchManagementForm(int matchId, int homeTeamId, int awayTeamId, string homeTeamName, string awayTeamName, DateTime matchDate)
        {
            InitializeComponent();
            _matchId = matchId;
            _homeTeamId = homeTeamId;
            _awayTeamId = awayTeamId;
            _homeTeamName = homeTeamName;
            _awayTeamName = awayTeamName;
            _matchDate = matchDate;
        }

        private void MatchManagementForm_Load(object sender, EventArgs e)
        {
            // Използваме само колоните от Properties дизайнера
            dgvEvents.AutoGenerateColumns = false;

            // Попълваме визуалните компоненти
            lblHomeTeam.Text = _homeTeamName;
            lblAwayTeam.Text = _awayTeamName;
            this.Text = $"Управление на мач: {_homeTeamName} - {_awayTeamName}";

            // Селектираме първото събитие ("Гол") от въведените в Properties -> Items
            if (cboEventType.Items.Count > 0) cboEventType.SelectedIndex = 0;

            // Зареждаме данните
            LoadMatchPlayers();
            RefreshEventsAndScore();
        }

        private void LoadMatchPlayers()
        {
            try
            {
                // 1. Взимаме данните от базата
                _playersTable = _matchesService.GetPlayersForMatch(_homeTeamId, _awayTeamId);

                // 2. Напълно изчистваме ComboBox-а
                cboPlayers.DataSource = null;
                cboPlayers.Items.Clear();

                // 3. Пълним менюто, като проверяваме как се казват колоните за име на играча
                foreach (DataRow row in _playersTable.Rows)
                {
                    string fullName = "";

                    // Проверка вариант 1: Ако в заявката ти колоната наистина се казва "full_name"
                    if (_playersTable.Columns.Contains("full_name"))
                    {
                        fullName = row["full_name"].ToString();
                    }
                    // Проверка вариант 2: Ако колоните са разделени на Име и Фамилия (най-често срещаното)
                    else if (_playersTable.Columns.Contains("first_name") && _playersTable.Columns.Contains("last_name"))
                    {
                        fullName = $"{row["first_name"]} {row["last_name"]}";
                    }
                    // Проверка вариант 3: Ако колоните са на български език в базата
                    else if (_playersTable.Columns.Contains("first_name_bg") || _playersTable.Columns.Contains("last_name_bg")) // или подобно
                    {
                        fullName = $"{row[1]} {row[2]}"; // Взема стойностите по пореден номер на колоната безопасно
                    }
                    else
                    {
                        // Защитен вариант: Ако не открие горните имена, взема първата текстова колона, която намери
                        fullName = row[1].ToString();
                    }

                    cboPlayers.Items.Add(fullName);
                }

                // 4. Започваме без избран играч по подразбиране
                cboPlayers.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при зареждане на футболисти: " + ex.Message, "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshEventsAndScore()
        {
            try
            {
                // 1. Презареждаме хронологията на събитията на екрана
                dgvEvents.DataSource = _matchesService.GetMatchEvents(_matchId);

                // 2. Обновяваме текстовия етикет на екрана (напр. "2 - 1")
                lblResult.Text = _matchesService.GetMatchScore(_matchId);

                // 3. КРИТИЧНОТО ДОПЪЛНЕНИЕ: Автоматично записваме този резултат в базата данни за таблицата matches!
                _matchesService.UpdateMatchResultFromEvents(_matchId, _matchDate);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при обновяване на хронологията и резултата: " + ex.Message, "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddEvent_Click(object sender, EventArgs e)
        {
            // 1. Проверяваме дали потребителят е избрал играч от списъка
            if (cboPlayers.SelectedIndex == -1 || cboEventType.SelectedItem == null)
            {
                MessageBox.Show("Моля, изберете събитие и футболист!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Тъй като ComboBox съдържа само текст, намираме съответния ред от 
            // таблицата в паметта чрез индекса на избрания елемент (SelectedIndex)
            if (_playersTable == null || cboPlayers.SelectedIndex >= _playersTable.Rows.Count) return;

            DataRow selectedPlayerRow = _playersTable.Rows[cboPlayers.SelectedIndex];

            string selectedEvent = cboEventType.SelectedItem.ToString();
            int playerId = Convert.ToInt32(selectedPlayerRow["id"]);
            int playerClubId = Convert.ToInt32(selectedPlayerRow["club_id"]);
            int minute = (int)nudMinute.Value;

            // Извикване на съответния метод от твоя MatchesService
            if (selectedEvent == "Гол") _matchesService.AddGoal(_matchId, playerId, playerClubId, minute);
            else if (selectedEvent == "Жълт картон" || selectedEvent == "Червен картон") _matchesService.AddCard(_matchId, playerId, selectedEvent, minute);
            else if (selectedEvent == "Фаул") _matchesService.AddFoul(_matchId, playerId, minute);

            RefreshEventsAndScore();

            // Автоматично вдигаме минутата с 1 на екрана за удобство
            nudMinute.Value = Math.Min(minute + 1, 120);
        }

        private void btnRemoveEvent_Click(object sender, EventArgs e)
        {
            if (dgvEvents.CurrentRow == null) return;

            if (MessageBox.Show("Сигурни ли сте, че искате да изтриете избраното събитие?", "Потвърждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var dataRow = (DataRowView)dgvEvents.CurrentRow.DataBoundItem;
                int id = Convert.ToInt32(dataRow["id"]);
                string eventType = dataRow["event_type"].ToString();

                _matchesService.DeleteEvent(id, eventType, _matchId);
                RefreshEventsAndScore();
            }
        }
    }
}