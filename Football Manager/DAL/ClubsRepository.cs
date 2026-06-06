using MySql.Data.MySqlClient;
using System.Data;

namespace Football_Manager.DAL
{
    public class ClubsRepository
    {
        public DataTable GetAll() => Db.GetTable("SELECT * FROM clubs ORDER BY name");

        public void Add(string name, string city, string stadium, string year)
        {
            string sql = "INSERT INTO clubs (name, city, stadium, founded_year) VALUES (@name, @city, @stadium, @year)";
            Db.Execute(sql, new[] {
                new MySqlParameter("@name", name),
                new MySqlParameter("@city", city),
                new MySqlParameter("@stadium", stadium),
                new MySqlParameter("@year", year)
            });
        }

        public void Update(int id, string name, string city, string stadium, string year)
        {
            string sql = "UPDATE clubs SET name=@name, city=@city, stadium=@stadium, founded_year=@year WHERE id=@id";
            Db.Execute(sql, new[] {
                new MySqlParameter("@id", id),
                new MySqlParameter("@name", name),
                new MySqlParameter("@city", city),
                new MySqlParameter("@stadium", stadium),
                new MySqlParameter("@year", year)
            });
        }

        public void Delete(int id)
        {
            // 1. Първо трием мачовете, за да не гърми Foreign Key грешката в matches
            string deleteMatchesSql = "DELETE FROM matches WHERE home_team_id = @id OR away_team_id = @id";
            Db.Execute(deleteMatchesSql, new[] { new MySqlParameter("@id", id) });

            // 2. След това трием играчите, които са записани в този клуб
            string deletePlayersSql = "DELETE FROM players WHERE club_id = @id";
            Db.Execute(deletePlayersSql, new[] { new MySqlParameter("@id", id) });

            // 3. Накрая трием самия клуб безопасно
            Db.Execute("DELETE FROM clubs WHERE id=@id", new[] { new MySqlParameter("@id", id) });
        }

    }
}