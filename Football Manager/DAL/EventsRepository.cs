using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Football_Manager.DAL
{
    public class EventsRepository
    {
        // Изискване 5.1 и 8: Взема играчите САМО на двата участващи отбора
        public DataTable GetPlayersByClubs(int homeClubId, int awayClubId)
        {
            string sql = @"
                SELECT p.id, CONCAT(p.full_name, ' (', c.name, ')') AS player_info, p.club_id
                FROM players p
                JOIN clubs c ON p.club_id = c.id
                WHERE p.club_id = @homeId OR p.club_id = @awayId
                ORDER BY c.name ASC, p.full_name ASC";

            MySqlParameter[] pars = {
                new MySqlParameter("@homeId", homeClubId),
                new MySqlParameter("@awayId", awayClubId)
            };
            return Db.GetTable(sql, pars);
        }

        // Хронология на мача: Обединява голове, картони и фаулове в една обща таблица подредена по минута
        public DataTable GetEventsByMatch(int matchId)
        {
            string sql = @"
        SELECT g.id, 'Гол' AS event_type, g.minute, p.full_name AS player_name, c.name AS club_name
        FROM match_goals g JOIN players p ON g.player_id = p.id JOIN clubs c ON g.club_id = c.id WHERE g.match_id = @matchId
        UNION ALL
        SELECT mc.id, mc.card_type AS event_type, mc.minute, p.full_name AS player_name, c.name AS club_name
        FROM match_cards mc JOIN players p ON mc.player_id = p.id JOIN clubs c ON p.club_id = c.id WHERE mc.match_id = @matchId
        UNION ALL
        SELECT mf.id, 'Фаул' AS event_type, mf.minute, p.full_name AS player_name, c.name AS club_name
        FROM match_fouls mf JOIN players p ON mf.player_id = p.id JOIN clubs c ON p.club_id = c.id WHERE mf.match_id = @matchId
        ORDER BY minute ASC";

            return Db.GetTable(sql, new[] { new MySqlParameter("@matchId", matchId) });
        }

        // Метод за изтриване
        public void DeleteEvent(string eventType, int id)
        {
            string table = eventType == "Гол" ? "match_goals" : (eventType == "Фаул" ? "match_fouls" : "match_cards");
            string sql = $"DELETE FROM {table} WHERE id = @id";
            Db.Execute(sql, new[] { new MySqlParameter("@id", id) });
        }

        public void AddGoal(int matchId, int playerId, int clubId, int minute)
        {
            string sql = "INSERT INTO match_goals (match_id, player_id, club_id, minute) VALUES (@mId, @pId, @cId, @min)";
            Db.Execute(sql, new[] {
                new MySqlParameter("@mId", matchId),
                new MySqlParameter("@pId", playerId),
                new MySqlParameter("@cId", clubId),
                new MySqlParameter("@min", minute)
            });
        }

        public void AddCard(int matchId, int playerId, string cardType, int minute)
        {
            string sql = "INSERT INTO match_cards (match_id, player_id, card_type, minute) VALUES (@mId, @pId, @type, @min)";
            Db.Execute(sql, new[] {
                new MySqlParameter("@mId", matchId),
                new MySqlParameter("@pId", playerId),
                new MySqlParameter("@type", cardType),
                new MySqlParameter("@min", minute)
            });
        }

        public void AddFoul(int matchId, int playerId, int minute)
        {
            string sql = "INSERT INTO match_fouls (match_id, player_id, minute, foul_type) VALUES (@mId, @pId, @min, 'Обикновено')";
            Db.Execute(sql, new[] {
                new MySqlParameter("@mId", matchId),
                new MySqlParameter("@pId", playerId),
                new MySqlParameter("@min", minute)
            });
        }

        // Изискване 5.3: Връща броя голове от събитията за даден отбор с цел валидация
        public int GetGoalCountForTeam(int matchId, int clubId)
        {
            string sql = "SELECT COUNT(*) FROM match_goals WHERE match_id = @mId AND club_id = @cId";
            DataTable dt = Db.GetTable(sql, new[] {
                new MySqlParameter("@mId", matchId),
                new MySqlParameter("@cId", clubId)
            });
            return dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0][0]) : 0;
        }
    }
}