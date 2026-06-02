using Football_Manager.DAL;
using Football_Manager.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace Football_Manager.BLL
{
    public class MatchesService
    {
        private readonly MatchesRepository _matchRepo = new MatchesRepository();
        private readonly EventsRepository _eventRepo = new EventsRepository();

        public DataTable GetMatches(int leagueId) => _matchRepo.GetMatchesByLeague(leagueId);

        // Използва твоя метод Update от MatchesRepository
        public void UpdateMatchResult(int matchId, int homeScore, int awayScore, DateTime matchDate)
        {
            Match m = new Match
            {
                Id = matchId,
                HomeScore = homeScore,
                AwayScore = awayScore,
                MatchDate = matchDate,
                IsPlayed = true
            };
            _matchRepo.Update(m);
        }

        public DataTable GetPlayersForMatch(int homeId, int awayId) => _eventRepo.GetPlayersByClubs(homeId, awayId);

        public DataTable GetMatchEvents(int matchId) => _eventRepo.GetEventsByMatch(matchId);

        public void AddGoal(int matchId, int playerId, int clubId, int minute) => _eventRepo.AddGoal(matchId, playerId, clubId, minute);

        public void AddCard(int matchId, int playerId, string cardType, int minute) => _eventRepo.AddCard(matchId, playerId, cardType, minute);

        public void AddFoul(int matchId, int playerId, int minute) => _eventRepo.AddFoul(matchId, playerId, minute);
        public void DeleteMatchEvent(string eventType, int id)
        {
            _eventRepo.DeleteEvent(eventType, id);
        }
        // Изискване 5.3: Проверка за логическа съгласуваност
        public bool ValidateScoreParity(int matchId, int inputHomeScore, int inputAwayScore)
        {
            // Намираме ID-тата на отборите от мача
            string sql = "SELECT home_team_id, away_team_id FROM matches WHERE id = @id";
            DataTable dt = Db.GetTable(sql, new[] { new MySqlParameter("@id", matchId) });
            if (dt.Rows.Count == 0) return true;

            int homeClubId = Convert.ToInt32(dt.Rows[0]["home_team_id"]);
            int awayClubId = Convert.ToInt32(dt.Rows[0]["away_team_id"]);

            int eventsHomeGoals = _eventRepo.GetGoalCountForTeam(matchId, homeClubId);
            int eventsAwayGoals = _eventRepo.GetGoalCountForTeam(matchId, awayClubId);

            return (inputHomeScore == eventsHomeGoals && inputAwayScore == eventsAwayGoals);
        }
    }
}