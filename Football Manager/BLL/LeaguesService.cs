using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using Football_Manager.DAL;
using Football_Manager.Models;
using FootballMatch = Football_Manager.Models.Match;

namespace Football_Manager.BLL
{
    public class LeaguesService
    {
        private readonly LeaguesRepository _leagueRepo = new LeaguesRepository();
        private readonly LeagueTeamsRepository _teamsRepo = new LeagueTeamsRepository();
        private readonly MatchesRepository _matchRepo = new MatchesRepository();

        // Бизнес логика за Лиги
        public DataTable GetLeagues() => _leagueRepo.GetAll();

        public bool SaveLeague(League league, bool isNew, out string message)
        {
            // Валидация за задължително текстово поле
            if (string.IsNullOrWhiteSpace(league.Name))
            {
                message = "Името на лигата не може да бъде празно!";
                return false;
            }

            // Валидация чрез регулярен израз за спазване на правилен футболен сезон
            if (!Regex.IsMatch(league.Season, @"^\d{4}/\d{4}$"))
            {
                message = "Сезонът трябва да бъде във формат ГГГГ/ГГГГ (напр. 2025/2026)!";
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

        // Бизнес логика за Участници в лигите
        public DataTable GetParticipants(int leagueId) => _teamsRepo.GetParticipants(leagueId);

        public DataTable GetAvailableClubs(int leagueId) => _teamsRepo.GetAvailableClubs(leagueId);

        public void AddClubToLeague(int leagueId, int clubId) => _teamsRepo.AddClubToLeague(leagueId, clubId);

        public void RemoveClubFromLeague(int leagueId, int clubId) => _teamsRepo.RemoveClubFromLeague(leagueId, clubId);

        // Генератор на Програма (Мачове)
        public DataTable GetSchedule(int leagueId) => _matchRepo.GetMatchesByLeague(leagueId);

        public bool GenerateFullSchedule(int leagueId, out string message)
        {
            try
            {
                DataTable dtTeams = _teamsRepo.GetParticipants(leagueId);
                List<int> teamIds = new List<int>();

                foreach (DataRow row in dtTeams.Rows)
                {
                    teamIds.Add(Convert.ToInt32(row["id"]));
                }

                // Защитна валидация преди стартиране на алгоритъма
                if (teamIds.Count < 2)
                {
                    message = "Трябва да има поне 2 отбора за генериране на програма!";
                    return false;
                }

                // Първо изчистваме старите срещи чрез DAL репозиторито
                _matchRepo.DeleteByLeague(leagueId);

                // Извикване на Round-Robin алгоритъма за генериране в паметта
                List<FootballMatch> schedule = CreateRoundRobin(leagueId, teamIds);

                // Записване на готовата колекция наведнъж в базата данни
                _matchRepo.SaveMatches(schedule);

                message = $"Програмата е генерирана! Общо кръгове: {schedule.Max(m => m.RoundNo)}";
                return true;
            }
            catch (Exception ex)
            {
                message = "Грешка: " + ex.Message;
                return false;
            }
        }

        public void UpdateMatchResult(Models.Match match) => _matchRepo.Update(match);

        // Алгоритмични частни методи (Round-Robin)
        private List<FootballMatch> CreateRoundRobin(int leagueId, List<int> teams)
        {
            List<FootballMatch> firstHalfMatches = new List<FootballMatch>();

            // Ако отборите са нечетен брой, добавяме "почиващ" отбор (-1)
            if (teams.Count % 2 != 0) teams.Add(-1);

            int numTeams = teams.Count;
            int numRounds = numTeams - 1;
            int matchesPerRound = numTeams / 2;

            // Изчисляване на следващата събота спрямо текущия ден
            DateTime startDate = DateTime.Today;
            int daysUntilSaturday = ((int)DayOfWeek.Saturday - (int)startDate.DayOfWeek + 7) % 7;
            DateTime firstSaturday = startDate.AddDays(daysUntilSaturday);

            List<int> tempTeams = new List<int>(teams);

            // Първи полусезон
            for (int round = 0; round < numRounds; round++)
            {
                DateTime roundDate = firstSaturday.AddDays(round * 7);

                for (int i = 0; i < matchesPerRound; i++)
                {
                    int home = tempTeams[i];
                    int away = tempTeams[numTeams - 1 - i];

                    if (home != -1 && away != -1)
                    {
                        firstHalfMatches.Add(new FootballMatch
                        {
                            LeagueId = leagueId,
                            RoundNo = round + 1,
                            HomeTeamId = home,
                            AwayTeamId = away,
                            MatchDate = roundDate
                        });
                    }
                }

                // Ротационна схема за следващия кръг
                int lastTeam = tempTeams[numTeams - 1];
                tempTeams.RemoveAt(numTeams - 1);
                tempTeams.Insert(1, lastTeam);
            }

            // Втори полусезон с разменени домакинства
            List<FootballMatch> secondHalfMatches = new List<FootballMatch>();
            foreach (var m in firstHalfMatches)
            {
                DateTime secondHalfDate = m.MatchDate.Value.AddDays(numRounds * 7);

                secondHalfMatches.Add(new FootballMatch
                {
                    LeagueId = leagueId,
                    RoundNo = m.RoundNo + numRounds,
                    HomeTeamId = m.AwayTeamId,
                    AwayTeamId = m.HomeTeamId,
                    MatchDate = secondHalfDate
                });
            }

            return firstHalfMatches.Concat(secondHalfMatches).ToList();
        }
    }
}