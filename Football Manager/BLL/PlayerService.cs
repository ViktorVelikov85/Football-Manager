using System;
using System.Data;
using Football_Manager.DAL;
using Football_Manager.Models;

namespace Football_Manager.BLL
{
    public class PlayerService
    {
        private readonly PlayersRepository _repo = new PlayersRepository();

        public DataTable GetPlayers(int? clubId = null, string position = "Всички", string search = "")
        {
            return _repo.GetFiltered(clubId, position, search);
        }

        public bool SavePlayer(Player player, bool isNew, out string msg)
        {
            if (string.IsNullOrWhiteSpace(player.FullName) || player.FullName.Split(' ').Length < 2)
            {
                msg = "Моля, въведете и двете имена на играча!";
                return false;
            }

            if (string.IsNullOrEmpty(player.Position) || player.Position == "Всички")
            {
                msg = "Моля, изберете валидна позиция!";
                return false;
            }

            if (player.ClubId <= 0)
            {
                msg = "Моля, изберете клуб!";
                return false;
            }

            if (player.BirthDate > DateTime.Now.AddYears(-14))
            {
                msg = "Играчът трябва да е поне на 14 години!";
                return false;
            }

            try
            {
                if (isNew) _repo.Add(player);
                else _repo.Update(player);

                msg = "Операцията е успешна!";
                return true;
            }
            catch (Exception ex)
            {
                msg = "Грешка в базата данни: " + ex.Message;
                return false;
            }
        }

        public void DeletePlayer(int id) => _repo.Delete(id);

        private readonly TransfersRepository _transferRepo = new TransfersRepository();

        public void ExecuteTransfer(int playerId, int? fromClubId, int toClubId, DateTime date, decimal fee)
        {
            // Тук може да добавите бизнес правила (напр. проверка на бюджет)
            if (playerId <= 0) throw new Exception("Невалиден играч!");

            _transferRepo.AddTransfer(playerId, fromClubId, toClubId, date, fee);
        }

        public DataTable GetTransferHistory(string searchTerm = "")
        {
            return _transferRepo.GetTransfers(searchTerm);
        }
        public DataTable GetTop3Scorers()
        {
            return _repo.GetTopScorers();
        }
    }
}