using Football_Manager.BLL;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Football_Manager.UI
{
    public partial class MatchManagementForm : Form
    {
        private readonly MatchesService _matchesService = new MatchesService();
        private readonly int _matchId, _homeTeamId, _awayTeamId;
        private readonly string _homeTeamName, _awayTeamName;
        private readonly DateTime _matchDate;

        public MatchManagementForm(int matchId, int homeTeamId, int awayTeamId, string homeTeamName, string awayTeamName, DateTime matchDate)
        {
            InitializeComponent();
            _matchId = matchId; _homeTeamId = homeTeamId; _awayTeamId = awayTeamId;
            _homeTeamName = homeTeamName; _awayTeamName = awayTeamName; _matchDate = matchDate;
        }

        private void MatchManagementForm_Load(object sender, EventArgs e)
        {
            // Попълваме отборите в двата отделни етикета
            lblHomeTeam.Text = _homeTeamName;
            lblAwayTeam.Text = _awayTeamName;

            this.Text = $"Управление на мач: {_homeTeamName} - {_awayTeamName}";

            // --- УЕДНАКВЯВАНЕ НА СТИЛА С ОСТАНАЛИТЕ ТАБЛИЦИ ---
            Font arial12 = new Font("Arial", 12);
            Font arial12Bold = new Font("Arial", 12, FontStyle.Bold);

            dgvEvents.DefaultCellStyle.Font = arial12;
            dgvEvents.ColumnHeadersDefaultCellStyle.Font = arial12Bold;
            dgvEvents.RowTemplate.Height = 35;

            // Зареждане на играчи
            cboPlayers.DataSource = _matchesService.GetPlayersForMatch(_homeTeamId, _awayTeamId);
            cboPlayers.DisplayMember = "player_info";
            cboPlayers.ValueMember = "id";

            RefreshEventsAndScore();
        }

        private void RefreshEventsAndScore()
        {
            DataTable dtEvents = _matchesService.GetMatchEvents(_matchId);
            dgvEvents.DataSource = dtEvents;

            // Настройка на колоните (Абсолютно същия подход като в твоя LeaguesForm)
            if (dgvEvents.Columns.Contains("id")) dgvEvents.Columns["id"].Visible = false;

            var cols = dgvEvents.Columns;

            if (cols.Contains("minute"))
            {
                cols["minute"].HeaderText = "Минута";
                cols["minute"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (cols.Contains("player_name"))
            {
                cols["player_name"].HeaderText = "Играч";
                cols["player_name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (cols.Contains("event_type"))
            {
                cols["event_type"].HeaderText = "Събитие";
                cols["event_type"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (cols.Contains("club_name"))
            {
                cols["club_name"].HeaderText = "Отбор";
                cols["club_name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            // Автоматично преброяване на головете
            int homeGoals = 0, awayGoals = 0;
            foreach (DataRow row in dtEvents.Rows)
            {
                if (row["event_type"].ToString() == "Gol" || row["event_type"].ToString() == "Гол")
                {
                    if (row["club_name"].ToString() == _homeTeamName) homeGoals++;
                    else if (row["club_name"].ToString() == _awayTeamName) awayGoals++;
                }
            }

            lblResult.Text = $"{homeGoals} - {awayGoals}";
            _matchesService.UpdateMatchResult(_matchId, homeGoals, awayGoals, _matchDate);
        }

        private void btnAddEvent_Click(object sender, EventArgs e)
        {
            if (cboPlayers.SelectedValue == null) return;

            string selectedEvent = cboEventType.SelectedItem.ToString();
            int playerId = Convert.ToInt32(cboPlayers.SelectedValue);
            int minute = (int)nudMinute.Value;
            int playerClubId = Convert.ToInt32(((DataRowView)cboPlayers.SelectedItem)["club_id"]);

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
                int id = Convert.ToInt32(dgvEvents.CurrentRow.Cells["id"].Value);
                string eventType = dgvEvents.CurrentRow.Cells["event_type"].Value.ToString();

                _matchesService.DeleteMatchEvent(eventType, id);
                RefreshEventsAndScore();
            }
        }

        private void lblAwayTeam_Click(object sender, EventArgs e)
        {

        }
    }
}