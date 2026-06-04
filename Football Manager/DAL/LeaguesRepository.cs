using Football_Manager.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Football_Manager.DAL
{
    public class LeaguesRepository
    {
        public DataTable GetAll()
        {
            string query = "SELECT id, name, season FROM leagues ORDER BY season DESC, name ASC";
            return Db.GetTable(query);
        }

        public void Add(League league)
        {
            string sql = "INSERT INTO leagues (name, season) VALUES (@name, @season)";
            Db.Execute(sql, GetParams(league));
        }

        public void Update(League league)
        {
            string sql = "UPDATE leagues SET name=@name, season=@season WHERE id=@id";
            var parameters = new List<MySqlParameter>(GetParams(league));
            parameters.Add(new MySqlParameter("@id", league.Id));
            Db.Execute(sql, parameters.ToArray());
        }

        public void Delete(int id)
        {
            Db.Execute("DELETE FROM leagues WHERE id = @id", new[] { new MySqlParameter("@id", id) });
        }

        private MySqlParameter[] GetParams(League league)
        {
            return new[] {
                new MySqlParameter("@name", league.Name),
                new MySqlParameter("@season", league.Season)
            };
        }
    }
}