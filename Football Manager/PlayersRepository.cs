using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Football_Manager
{
    internal class PlayersRepository
    {
        // Метод за извличане на играчи с комбинирани филтри
        public DataTable GetPlayers(int? clubId = null, string position = null, string searchName = null)
        {
            // JOIN ни позволява да вземем името на клуба от таблицата clubs
            string sql = @"SELECT p.id, p.full_name, c.name AS club_name, p.position, p.birth_date, p.shirt_number 
                       FROM players p 
                       JOIN clubs c ON p.club_id = c.id WHERE 1=1";

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            // Динамично добавяне на филтри
            if (clubId.HasValue && clubId > 0)
            {
                sql += " AND p.club_id = @clubId";
                parameters.Add(new MySqlParameter("@clubId", clubId));
            }

            if (!string.IsNullOrEmpty(position) && position != "Всички")
            {
                sql += " AND p.position = @position";
                parameters.Add(new MySqlParameter("@position", position));
            }

            if (!string.IsNullOrEmpty(searchName))
            {
                sql += " AND p.full_name LIKE @name";
                parameters.Add(new MySqlParameter("@name", "%" + searchName + "%"));
            }

            return Db.GetTable(sql, parameters.ToArray());
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

        // Редактиране на съществуващ играч
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
            string sql = "DELETE FROM players WHERE id = @id";
            Db.Execute(sql, new[] { new MySqlParameter("@id", id) });
        }
    }
}
