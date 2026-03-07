using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Football_Manager
{
    public static class Db
    {
        // При XAMPP потребителят е root, а паролата е празна
        private static string connString = "server=localhost;database=football_manager;uid=root;pwd=;charset=utf8;";

        public static MySqlConnection GetConnection() => new MySqlConnection(connString);

        public static DataTable GetDataTable(string sql, MySqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            using (var conn = GetConnection())
            {
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public static void Execute(string sql, MySqlParameter[] parameters = null)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static DataTable GetTable(string sql, MySqlParameter[] parameters = null)
        {
            DataTable table = new DataTable();
            using (MySqlConnection conn = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(table);
            }
            return table;
        }
    }
}
