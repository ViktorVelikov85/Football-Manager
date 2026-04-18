using System;
using System.Data;
using System.Text.RegularExpressions;
using Football_Manager.DAL;
using Football_Manager.Models;

namespace Football_Manager.BLL
{
    public class LeagueService
    {
        private readonly LeaguesRepository _leagueRepo = new LeaguesRepository();
        private readonly LeagueTeamsRepository _teamsRepo = new LeagueTeamsRepository();

        // --- Управление на лиги ---

        public DataTable GetLeagues() => _leagueRepo.GetAll();

        public bool SaveLeague(League league, bool isNew, out string message)
        {
            // 1. Проверка за празно име
            if (string.IsNullOrWhiteSpace(league.Name))
            {
                message = "Името на лигата не може да бъде празно!";
                return false;
            }

            // 2. Валидация на сезона (Regex за формат YYYY/YYYY)
            if (!Regex.IsMatch(league.Season, @"^\d{4}/\d{4}$"))
            {
                message = "Сезонът трябва да бъде във формат ГГГГ/ГГГГ (напр. 2025/2026)!";
                return false;
            }

            // 3. Проверка за уникалност (само при нова лига)
            if (isNew && _leagueRepo.Exists(league.Name, league.Season))
            {
                message = "Вече съществува лига с това име за този сезон!";
                return false;
            }

            try
            {
                if (isNew) _leagueRepo.Add(league);
                else _leagueRepo.Update(league);

                message = "Успешен запис!";
                return true;
            }
            catch (Exception ex)
            {
                message = "Грешка при запис: " + ex.Message;
                return false;
            }
        }

        public void DeleteLeague(int id) => _leagueRepo.Delete(id);

        // --- Управление на участници ---

        public DataTable GetParticipants(int leagueId) => _teamsRepo.GetParticipants(leagueId);

        public DataTable GetAvailableClubs(int leagueId) => _teamsRepo.GetAvailableClubs(leagueId);

        public void AddClubToLeague(int leagueId, int clubId)
        {
            // Тук може да добавиш проверка дали лигата не е "запълнена" (напр. макс 16 отбора)
            _teamsRepo.AddClubToLeague(leagueId, clubId);
        }

        public void RemoveClubFromLeague(int leagueId, int clubId)
        {
            // ВАЖНО: Тук ще добавим проверка за изиграни мачове в следващите етапи
            _teamsRepo.RemoveClubFromLeague(leagueId, clubId);
        }
    }
}