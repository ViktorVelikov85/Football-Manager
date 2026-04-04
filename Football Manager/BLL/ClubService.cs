using System;
using System.Data;
using Football_Manager.DAL;
using Football_Manager.Models;

namespace Football_Manager.BLL
{
    public class ClubService
    {
        private readonly ClubsRepository _repo = new ClubsRepository();

        // Имената на методите тук трябва да съвпадат с тези, които викаш в ClubsForm
        public DataTable GetAllClubs() => _repo.GetAll();

        public bool SaveClub(Club club, bool isNew, out string msg)
        {
            if (!IsValid(club, out msg)) return false;

            try
            {
                if (isNew)
                {
                    _repo.Add(club.Name, club.City, club.Stadium, club.FoundedYear);
                    msg = "Клубът е добавен успешно!";
                }
                else
                {
                    _repo.Update(club.Id, club.Name, club.City, club.Stadium, club.FoundedYear);
                    msg = "Данните бяха обновени!";
                }
                return true;
            }
            catch (Exception ex)
            {
                msg = ex.Message.Contains("Duplicate entry") ? "Вече съществува такъв клуб!" : "Грешка: " + ex.Message;
                return false;
            }
        }

        public void DeleteClub(int id) => _repo.Delete(id);

        private bool IsValid(Club club, out string msg)
        {
            if (string.IsNullOrWhiteSpace(club.Name) || string.IsNullOrWhiteSpace(club.City))
            {
                msg = "Моля, попълнете Име и Град!";
                return false;
            }
            msg = "";
            return true;
        }
    }
}