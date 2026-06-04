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
            dgvEvents.AutoGenerateColumns = false;

            lblHomeTeam.Text = _homeTeamName;
            lblAwayTeam.Text = _awayTeamName;
            this.Text = $"Управление на мач: {_homeTeamName} - {_awayTeamName}";

            if (cboEventType.Items.Count > 0) cboEventType.SelectedIndex = 0;

            LoadMatchPlayers();
            RefreshEventsAndScore();
        }

        private void LoadMatchPlayers()
        {
            try
            {
                _playersTable = _matchesService.GetPlayersForMatch(_homeTeamId, _awayTeamId);

                cboPlayers.DataSource = null;
                cboPlayers.Items.Clear();

                foreach (DataRow row in _playersTable.Rows)
                {
                    string fullName = "";

                    if (_playersTable.Columns.Contains("full_name"))
                    {
                        fullName = row["full_name"].ToString();
                    }
                    else if (_playersTable.Columns.Contains("first_name") && _playersTable.Columns.Contains("last_name"))
                    {
                        fullName = $"{row["first_name"]} {row["last_name"]}";
                    }
                    else if (_playersTable.Columns.Contains("first_name_bg") || _playersTable.Columns.Contains("last_name_bg"))
                    {
                        fullName = $"{row[1]} {row[2]}";
                    }
                    else
                    {
                        fullName = row[1].ToString();
                    }

                    cboPlayers.Items.Add(fullName);
                }

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
                // Презареждаме хронологията на събитията на екрана
                dgvEvents.DataSource = _matchesService.GetMatchEvents(_matchId);

                // Обновяваме текстовия резултат на екрана
                lblResult.Text = _matchesService.GetMatchScore(_matchId);

                // Автоматично записваме новия резултат в таблицата matches
                _matchesService.UpdateMatchResultFromEvents(_matchId, _matchDate);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при обновяване на хронологията и резултата: " + ex.Message, "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddEvent_Click(object sender, EventArgs e)
        {
            if (cboPlayers.SelectedIndex == -1 || cboEventType.SelectedItem == null)
            {
                MessageBox.Show("Моля, изберете събитие и футболист!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_playersTable == null || cboPlayers.SelectedIndex >= _playersTable.Rows.Count) return;

            DataRow selectedPlayerRow = _playersTable.Rows[cboPlayers.SelectedIndex];

            string selectedEvent = cboEventType.SelectedItem.ToString();
            int playerId = Convert.ToInt32(selectedPlayerRow["id"]);
            int playerClubId = Convert.ToInt32(selectedPlayerRow["club_id"]);
            int minute = (int)nudMinute.Value;

            if (selectedEvent == "Гол") _matchesService.AddGoal(_matchId, playerId, playerClubId, minute);
            else if (selectedEvent == "Жълт картон" || selectedEvent == "Червен картон") _matchesService.AddCard(_matchId, playerId, selectedEvent, minute);
            else if (selectedEvent == "Фаул") _matchesService.AddFoul(_matchId, playerId, minute);

            RefreshEventsAndScore();

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