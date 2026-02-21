using MySql.Data.MySqlClient;
using System.Data;

namespace Football_Manager
{
    public class ClubsRepository
    {
        public DataTable GetAll() => Db.GetDataTable("SELECT * FROM clubs ORDER BY name");

        public void Add(string name, string city)
        {
            string sql = "INSERT INTO clubs (name, city) VALUES (@name, @city)";
            Db.Execute(sql, new[] { new MySqlParameter("@name", name), new MySqlParameter("@city", city) });
        }

        public void Update(int id, string name, string city)
        {
            string sql = "UPDATE clubs SET name=@name, city=@city WHERE id=@id";
            Db.Execute(sql, new[] { new MySqlParameter("@name", name), new MySqlParameter("@city", city), new MySqlParameter("@id", id) });
        }

        public void Delete(int id)
        {
            string sql = "DELETE FROM clubs WHERE id=@id";
            Db.Execute(sql, new[] { new MySqlParameter("@id", id) });
        }
    }
}