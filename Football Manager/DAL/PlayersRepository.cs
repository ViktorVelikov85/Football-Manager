using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

namespace Football_Manager.DAL
{
    public class PlayersRepository
    {
        public DataTable GetFiltered(int? clubId, string position, string searchTerm)
        {
            string query = @"SELECT p.id, p.full_name, c.name as club_name, p.club_id, 
                                    p.position, p.shirt_number, p.birth_date, p.status 
                             FROM players p 
                             JOIN clubs c ON p.club_id = c.id WHERE 1=1";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            if (clubId.HasValue && clubId.Value > 0)
            {
                query += " AND p.club_id = @clubId";
                parameters.Add(new MySqlParameter("@clubId", clubId.Value));
            }

            if (!string.IsNullOrEmpty(position) && position != "Всички")
            {
                query += " AND p.position = @pos";
                parameters.Add(new MySqlParameter("@pos", position));
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query += " AND p.full_name LIKE @search";
                parameters.Add(new MySqlParameter("@search", "%" + searchTerm + "%"));
            }

            query += " ORDER BY p.full_name ASC";
            return Db.GetTable(query, parameters.ToArray());
        }

        public void Add(Models.Player p)
        {
            string sql = @"INSERT INTO players (club_id, full_name, birth_date, position, shirt_number, status) 
                           VALUES (@clubId, @fullName, @birthDate, @position, @shirtNumber, @status)";
            Db.Execute(sql, GetParams(p));
        }

        public void Update(Models.Player p)
        {
            string sql = @"UPDATE players SET club_id=@clubId, full_name=@fullName, 
                           birth_date=@birthDate, position=@position, shirt_number=@shirtNumber, status=@status 
                           WHERE id=@id";
            var parameters = new List<MySqlParameter>(GetParams(p));
            parameters.Add(new MySqlParameter("@id", p.Id));
            Db.Execute(sql, parameters.ToArray());
        }

        public void Delete(int id)
        {
            Db.Execute("DELETE FROM players WHERE id = @id", new[] { new MySqlParameter("@id", id) });
        }

        private MySqlParameter[] GetParams(Models.Player p)
        {
            return new[] {
                new MySqlParameter("@clubId", p.ClubId),
                new MySqlParameter("@fullName", p.FullName),
                new MySqlParameter("@birthDate", p.BirthDate.ToString("yyyy-MM-dd")),
                new MySqlParameter("@position", p.Position),
                new MySqlParameter("@shirtNumber", p.ShirtNumber),
                new MySqlParameter("@status", p.Status)
            };
        }
        public DataTable GetTopScorers()
        {
            // Използваме LEFT JOIN и филтрираме празни играчи, подреждаме правилно
            string query = @"
                SELECT p.full_name, COUNT(g.id) AS goals_count
                FROM match_goals g
                JOIN players p ON g.player_id = p.id
                GROUP BY p.id, p.full_name
                ORDER BY goals_count DESC
                LIMIT 3";

            return Db.GetTable(query);
        }
        public DataTable GetTopPlayersByCards()
        {
            string query = @"
                SELECT full_name, yellow_cards, red_cards
                FROM (
                    SELECT 
                        p.full_name,
                        SUM(CASE WHEN c.card_type = 'Жълт картон' THEN 1 ELSE 0 END) AS yellow_cards,
                        SUM(CASE WHEN c.card_type = 'Червен картон' THEN 1 ELSE 0 END) AS red_cards
                    FROM match_cards c
                    JOIN players p ON p.id = c.player_id
                    GROUP BY p.id, p.full_name
                ) AS sub
                WHERE (yellow_cards + red_cards) > 0
                ORDER BY (yellow_cards + red_cards) DESC
                LIMIT 5";

            return Db.GetTable(query);
        }
    }
}