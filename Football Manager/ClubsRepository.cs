using MySql.Data.MySqlClient;
using System.Data;

namespace Football_Manager
{
    public class ClubsRepository
    {
        public DataTable GetAll() => Db.GetDataTable("SELECT * FROM clubs ORDER BY name");

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
            string sql = "DELETE FROM clubs WHERE id=@id";
            Db.Execute(sql, new[] { new MySqlParameter("@id", id) });
        }
    }
}