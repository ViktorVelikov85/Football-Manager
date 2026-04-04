using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Football_Manager.DAL
{
    internal class TransfersRepository
    {
        public void AddTransfer(int playerId, int? fromClubId, int toClubId, DateTime date, decimal fee)
        {
            // 1. Запис в историята на трансферите
            // Забележка: Увери се, че имената на колоните (PlayerId, FromClubId...) съвпадат с базата ти
            string sqlTransfer = @"INSERT INTO transfers (PlayerId, FromClubId, ToClubId, TransferDate, Fee) 
                                 VALUES (@pId, @fId, @tId, @date, @fee)";

            MySqlParameter[] transferParams = new MySqlParameter[]
            {
                new MySqlParameter("@pId", playerId),
                new MySqlParameter("@fId", (object)fromClubId ?? DBNull.Value),
                new MySqlParameter("@tId", toClubId),
                new MySqlParameter("@date", date),
                new MySqlParameter("@fee", fee)
            };

            Db.Execute(sqlTransfer, transferParams);

            // 2. Обновяване на текущия клуб на играча в таблицата players
            string sqlUpdatePlayer = "UPDATE players SET club_id = @tId WHERE id = @pId";

            MySqlParameter[] updateParams = new MySqlParameter[]
            {
                new MySqlParameter("@tId", toClubId),
                new MySqlParameter("@pId", playerId)
            };

            Db.Execute(sqlUpdatePlayer, updateParams);
        }

        public DataTable GetTransfers(string searchTerm = "")
        {
            // Използваме LEFT JOIN за FromClubId, защото ако е бил "Свободен агент", ID-то е NULL
            string sql = @"SELECT p.full_name AS 'Име на играч', 
                                  COALESCE(c1.name, 'Свободен агент') AS 'От клуб', 
                                  c2.name AS 'Към клуб', 
                                  t.TransferDate AS 'Дата', 
                                  t.Fee AS 'Такса' 
                           FROM transfers t
                           JOIN players p ON t.PlayerId = p.id
                           LEFT JOIN clubs c1 ON t.FromClubId = c1.id
                           JOIN clubs c2 ON t.ToClubId = c2.id";

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                sql += " WHERE p.full_name LIKE @search ORDER BY t.TransferDate DESC";
                return Db.GetTable(sql, new MySqlParameter[] { new MySqlParameter("@search", "%" + searchTerm + "%") });
            }

            sql += " ORDER BY t.TransferDate DESC";
            return Db.GetTable(sql);
        }
    }
}