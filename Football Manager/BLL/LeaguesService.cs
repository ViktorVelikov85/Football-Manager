using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using Football_Manager.DAL;
using Football_Manager.Models;

// Решаваме конфликта между Football_Manager.Models.Match и System.Text.RegularExpressions.Match
using FootballMatch = Football_Manager.Models.Match;

namespace Football_Manager.BLL
{
    public class LeaguesService
    {
        private readonly LeaguesRepository _leagueRepo = new LeaguesRepository();
        private readonly LeagueTeamsRepository _teamsRepo = new LeagueTeamsRepository();
        private readonly MatchesRepository _matchRepo = new MatchesRepository();

        // --- Управление на лиги ---

        public DataTable GetLeagues() => _leagueRepo.GetAll();

        public bool SaveLeague(League league, bool isNew, out string message)
        {
            if (string.IsNullOrWhiteSpace(league.Name))
            {
                message = "Името на лигата не може да бъде празно!";
                return false;
            }

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

        // --- Управление на участници ---

        public DataTable GetParticipants(int leagueId) => _teamsRepo.GetParticipants(leagueId);

        public DataTable GetAvailableClubs(int leagueId) => _teamsRepo.GetAvailableClubs(leagueId);

        public void AddClubToLeague(int leagueId, int clubId)
        {
            _teamsRepo.AddClubToLeague(leagueId, clubId);
        }

        public void RemoveClubFromLeague(int leagueId, int clubId)
        {
            _teamsRepo.RemoveClubFromLeague(leagueId, clubId);
        }


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

                if (teamIds.Count < 2)
                {
                    message = "Трябва да има поне 2 отбора!";
                    return false;
                }

                _matchRepo.DeleteByLeague(leagueId);

                // Извикваме обновения алгоритъм
                List<FootballMatch> schedule = CreateRoundRobin(leagueId, teamIds);

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

        private List<FootballMatch> CreateRoundRobin(int leagueId, List<int> teams)
        {
            List<FootballMatch> firstHalfMatches = new List<FootballMatch>();
            if (teams.Count % 2 != 0) teams.Add(-1);

            int numTeams = teams.Count;
            int numRounds = numTeams - 1;
            int matchesPerRound = numTeams / 2;

            // --- ЛОГИКА ЗА ДАТИТЕ ---
            DateTime startDate = DateTime.Today;
            // Намираме колко дни остават до първата събота (0 ако днес е събота)
            int daysUntilSaturday = ((int)DayOfWeek.Saturday - (int)startDate.DayOfWeek + 7) % 7;
            DateTime firstSaturday = startDate.AddDays(daysUntilSaturday);

            List<int> tempTeams = new List<int>(teams);

            for (int round = 0; round < numRounds; round++)
            {
                // Дата за текущия кръг (всяка събота)
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
                            MatchDate = roundDate // Записваме датата
                        });
                    }
                }

                // Ротация
                int lastTeam = tempTeams[numTeams - 1];
                tempTeams.RemoveAt(numTeams - 1);
                tempTeams.Insert(1, lastTeam);
            }

            // Втори полусезон
            List<FootballMatch> secondHalfMatches = new List<FootballMatch>();
            foreach (var m in firstHalfMatches)
            {
                // Датите продължават след първия полусезон
                DateTime secondHalfDate = m.MatchDate.Value.AddDays(numRounds * 7);

                secondHalfMatches.Add(new FootballMatch
                {
                    LeagueId = leagueId,
                    RoundNo = m.RoundNo + numRounds,
                    HomeTeamId = m.AwayTeamId,
                    AwayTeamId = m.HomeTeamId,
                    MatchDate = secondHalfDate // Записваме датата за втория полусезон
                });
            }

            return firstHalfMatches.Concat(secondHalfMatches).ToList();
        }
        public void UpdateMatchResult(Models.Match match)
        {
            // Тук викаме репозиторито
            _matchRepo.Update(match);
        }
    }
}