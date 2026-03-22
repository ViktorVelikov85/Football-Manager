using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Football_Manager
{
    internal class TransfersRepository
    {
        public void AddTransfer(int playerId, int? fromClubId, int toClubId, DateTime date, decimal fee)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Запис в историята
                        string sqlTransfer = @"INSERT INTO transfers (PlayerId, FromClubId, ToClubId, TransferDate, Fee) 
                                             VALUES (@pId, @fId, @tId, @date, @fee)";
                        using (var cmd1 = new MySqlCommand(sqlTransfer, conn, transaction))
                        {
                            cmd1.Parameters.AddWithValue("@pId", playerId);
                            cmd1.Parameters.AddWithValue("@fId", (object)fromClubId ?? DBNull.Value);
                            cmd1.Parameters.AddWithValue("@tId", toClubId);
                            cmd1.Parameters.AddWithValue("@date", date);
                            cmd1.Parameters.AddWithValue("@fee", fee);
                            cmd1.ExecuteNonQuery();
                        }

                        // 2. Ъпдейт на играча
                        string sqlUpdate = "UPDATE players SET club_id = @tId WHERE id = @pId";
                        using (var cmd2 = new MySqlCommand(sqlUpdate, conn, transaction))
                        {
                            cmd2.Parameters.AddWithValue("@tId", toClubId);
                            cmd2.Parameters.AddWithValue("@pId", playerId);
                            cmd2.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Грешка при трансфера: " + ex.Message);
                    }
                }
            }
        }

        public DataTable GetTransfers(string searchTerm = "")
        {
            string sql = @"SELECT p.full_name AS 'Име на играч', 
                                  COALESCE(c1.name, 'Свободен агент') AS 'От клуб', 
                                  c2.name AS 'Към клуб', 
                                  t.TransferDate AS 'Дата', 
                                  t.Fee AS 'Такса' 
                           FROM transfers t
                           JOIN players p ON t.PlayerId = p.id
                           LEFT JOIN clubs c1 ON t.FromClubId = c1.id
                           JOIN clubs c2 ON t.ToClubId = c2.id WHERE 1=1";

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                sql += " AND p.full_name LIKE @search";
                sql += " ORDER BY t.TransferDate DESC";
                return Db.GetTable(sql, new[] { new MySqlParameter("@search", "%" + searchTerm + "%") });
            }

            sql += " ORDER BY t.TransferDate DESC";
            return Db.GetTable(sql);
        }
    }
}