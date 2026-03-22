using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

namespace Football_Manager
{
    internal class PlayersRepository
    {
        public DataTable GetPlayers() => GetFilteredPlayers(null, "Всички", "");

        public DataTable GetFilteredPlayers(int? clubId, string position, string searchTerm)
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

        public void Add(int clubId, string fullName, string birthDate, string position, int shirtNumber, string status)
        {
            string sql = @"INSERT INTO players (club_id, full_name, birth_date, position, shirt_number, status) 
                           VALUES (@clubId, @fullName, @birthDate, @position, @shirtNumber, @status)";
            Db.Execute(sql, new[] {
                new MySqlParameter("@clubId", clubId),
                new MySqlParameter("@fullName", fullName),
                new MySqlParameter("@birthDate", birthDate),
                new MySqlParameter("@position", position),
                new MySqlParameter("@shirtNumber", shirtNumber),
                new MySqlParameter("@status", status)
            });
        }

        public void Update(int id, int clubId, string fullName, string birthDate, string position, int shirtNumber, string status)
        {
            string sql = @"UPDATE players SET club_id=@clubId, full_name=@fullName, 
                           birth_date=@birthDate, position=@position, shirt_number=@shirtNumber, status=@status 
                           WHERE id=@id";
            Db.Execute(sql, new[] {
                new MySqlParameter("@id", id),
                new MySqlParameter("@clubId", clubId),
                new MySqlParameter("@fullName", fullName),
                new MySqlParameter("@birthDate", birthDate),
                new MySqlParameter("@position", position),
                new MySqlParameter("@shirtNumber", shirtNumber),
                new MySqlParameter("@status", status)
            });
        }

        public void Delete(int id)
        {
            Db.Execute("DELETE FROM players WHERE id = @id", new[] { new MySqlParameter("@id", id) });
        }

        public void UpdatePlayerClub(int playerId, int newClubId)
        {
            Db.Execute("UPDATE players SET club_id = @newClubId WHERE id = @playerId", new[] {
                new MySqlParameter("@newClubId", newClubId),
                new MySqlParameter("@playerId", playerId)
            });
        }
    }
}