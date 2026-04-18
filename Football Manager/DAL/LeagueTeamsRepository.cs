using MySql.Data.MySqlClient;
using System.Data;

namespace Football_Manager.DAL
{
    public class LeagueTeamsRepository
    {
        public DataTable GetParticipants(int leagueId)
        {
            string query = @"
                SELECT c.id, c.name, c.city 
                FROM clubs c
                JOIN league_teams lt ON c.id = lt.club_id
                WHERE lt.league_id = @leagueId 
                ORDER BY c.name ASC";

            return Db.GetTable(query, new[] { new MySqlParameter("@leagueId", leagueId) });
        }

        public DataTable GetAvailableClubs(int leagueId)
        {
            string query = @"
                SELECT id, name 
                FROM clubs 
                WHERE id NOT IN (SELECT club_id FROM league_teams WHERE league_id = @leagueId)
                ORDER BY name ASC";

            return Db.GetTable(query, new[] { new MySqlParameter("@leagueId", leagueId) });
        }

        public void AddClubToLeague(int leagueId, int clubId)
        {
            string sql = "INSERT INTO league_teams (league_id, club_id) VALUES (@leagueId, @clubId)";
            Db.Execute(sql, new[] {
                new MySqlParameter("@leagueId", leagueId),
                new MySqlParameter("@clubId", clubId)
            });
        }

        public void RemoveClubFromLeague(int leagueId, int clubId)
        {
            string sql = "DELETE FROM league_teams WHERE league_id = @leagueId AND club_id = @clubId";
            Db.Execute(sql, new[] {
                new MySqlParameter("@leagueId", leagueId),
                new MySqlParameter("@clubId", clubId)
            });
        }
    }
}