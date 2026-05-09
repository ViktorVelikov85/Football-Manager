using Football_Manager.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

namespace Football_Manager.DAL
{
    public class MatchesRepository
    {
        public DataTable GetMatchesByLeague(int leagueId)
        {
           
            string sql = @"SELECT m.id, m.round_no, 
                         m.home_team_id, m.away_team_id,
                         h.name AS home_team, 
                         a.name AS away_team, 
                         m.home_score, m.away_score, 
                         m.match_date, m.is_played
                  FROM matches m
                  JOIN clubs h ON m.home_team_id = h.id
                  JOIN clubs a ON m.away_team_id = a.id
                  WHERE m.league_id = @leagueId
                  ORDER BY m.round_no ASC, m.id ASC";

            return Db.GetTable(sql, new[] { new MySqlParameter("@leagueId", leagueId) });
        }

        // Изтрива цялата програма за дадена лига (преди прегенериране)
        public void DeleteByLeague(int leagueId)
        {
            string sql = "DELETE FROM matches WHERE league_id = @leagueId";
            Db.Execute(sql, new[] { new MySqlParameter("@leagueId", leagueId) });
        }

        // Записва списък от мачове (програмата) в базата данни
        public void SaveMatches(List<Match> matches)
        {
            foreach (var m in matches)
            {
                // Добавяме match_date в INSERT заявката
                string sql = @"INSERT INTO matches (league_id, round_no, home_team_id, away_team_id, match_date) 
                       VALUES (@leagueId, @roundNo, @homeId, @awayId, @mDate)";

                MySqlParameter[] ps = {
                new MySqlParameter("@leagueId", m.LeagueId),
                new MySqlParameter("@roundNo", m.RoundNo),
                new MySqlParameter("@homeId", m.HomeTeamId),
                new MySqlParameter("@awayId", m.AwayTeamId),
                new MySqlParameter("@mDate", (object)m.MatchDate ?? DBNull.Value)
            };
                Db.Execute(sql, ps);
            }
        }
        public void Update(Models.Match m)
        {
            string sql = @"UPDATE matches 
                   SET home_score = @hScore, away_score = @aScore, 
                       is_played = @isPlayed, match_date = @mDate 
                   WHERE id = @id";

            MySqlParameter[] ps = {
                new MySqlParameter("@hScore", (object)m.HomeScore ?? DBNull.Value),
                new MySqlParameter("@aScore", (object)m.AwayScore ?? DBNull.Value),
                new MySqlParameter("@isPlayed", m.IsPlayed),
                new MySqlParameter("@mDate", (object)m.MatchDate ?? DBNull.Value),
                new MySqlParameter("@id", m.Id)
            };
            Db.Execute(sql, ps);
        }
    }
}