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
            // Използваме LEFT JOIN за стария клуб, тъй като при 'Free Agent' стойността в базата е NULL
            string sql = @"SELECT p.full_name AS player_name, 
                                  COALESCE(c1.name, 'Free Agent') AS old_club_name, 
                                  c2.name AS new_club_name, 
                                  t.TransferDate AS transfer_date, 
                                  t.Fee AS transfer_fee 
                           FROM transfers t
                           JOIN players p ON t.PlayerId = p.id
                           LEFT JOIN clubs c1 ON t.FromClubId = c1.id
                           JOIN clubs c2 ON t.ToClubId = c2.id";

            // Ако има въведена дума за търсене, филтрираме по името на играча
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                sql += " WHERE p.full_name LIKE @search ORDER BY t.TransferDate DESC";
                return Db.GetTable(sql, new MySqlParameter[] { new MySqlParameter("@search", "%" + searchTerm + "%") });
            }

            // В противен случай сортираме хронологично от най-новите към най-старите трансфери
            sql += " ORDER BY t.TransferDate DESC";
            return Db.GetTable(sql);
        }
    }
}