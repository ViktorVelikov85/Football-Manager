using Football_Manager.DAL;
using Football_Manager.Models;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Football_Manager.BLL
{
    public class MatchesService
    {
        private readonly MatchesRepository _matchRepo = new MatchesRepository();
        private readonly EventsRepository _eventRepo = new EventsRepository();

        public DataTable GetMatches(int leagueId) => _matchRepo.GetMatchesByLeague(leagueId);

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

        public void DeleteEvent(int id, string eventType, int matchId)
        {
            _eventRepo.DeleteEvent(eventType, id);
        }

        public string GetMatchScore(int matchId)
        {
            try
            {
                string sql = "SELECT home_team_id, away_team_id FROM matches WHERE id = @id";
                DataTable dt = Db.GetTable(sql, new[] { new MySqlParameter("@id", matchId) });
                if (dt.Rows.Count == 0) return "0 - 0";

                int homeClubId = Convert.ToInt32(dt.Rows[0]["home_team_id"]);
                int awayClubId = Convert.ToInt32(dt.Rows[0]["away_team_id"]);

                int homeGoals = _eventRepo.GetGoalCountForTeam(matchId, homeClubId);
                int awayGoals = _eventRepo.GetGoalCountForTeam(matchId, awayClubId);

                return $"{homeGoals} - {awayGoals}";
            }
            catch
            {
                return "0 - 0";
            }
        }

        public void UpdateMatchResultFromEvents(int matchId, DateTime matchDate)
        {
            try
            {
                // Намираме ID-тата на двата отбора за този мач
                string sqlTeams = "SELECT home_team_id, away_team_id FROM matches WHERE id = @id";
                DataTable dt = Db.GetTable(sqlTeams, new[] { new MySqlParameter("@id", matchId) });
                if (dt.Rows.Count == 0) return;

                int homeClubId = Convert.ToInt32(dt.Rows[0]["home_team_id"]);
                int awayClubId = Convert.ToInt32(dt.Rows[0]["away_team_id"]);

                // Броим головете от събитията за всеки отбор
                int homeScore = _eventRepo.GetGoalCountForTeam(matchId, homeClubId);
                int awayScore = _eventRepo.GetGoalCountForTeam(matchId, awayClubId);

                // Записваме обновения резултат обратно в базата
                UpdateMatchResult(matchId, homeScore, awayScore, matchDate);
            }
            catch (Exception ex)
            {
                throw new Exception("Грешка при автоматичното обновяване на резултата: " + ex.Message);
            }
        }
    }
}