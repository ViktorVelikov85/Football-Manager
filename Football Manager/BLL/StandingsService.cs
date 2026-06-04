using Football_Manager.DAL;
using Football_Manager.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Football_Manager.BLL
{
    public class StandingsService
    {
        private readonly MatchesRepository _matchesRepo = new MatchesRepository();
        private readonly LeaguesService _leaguesService = new LeaguesService();

        public List<Standing> GetStandings(int leagueId)
        {
            // 1. Вземаме всички отбори в тази лига, за да присъстват в класирането (дори с 0 мача)
            DataTable dtClubs = _leaguesService.GetParticipants(leagueId);
            var standingsDict = new Dictionary<int, Standing>();

            foreach (DataRow row in dtClubs.Rows)
            {
                int clubId = Convert.ToInt32(row["id"]);
                standingsDict[clubId] = new Standing
                {
                    ClubId = clubId,
                    ClubName = row["name"].ToString()
                };
            }

            // 2. Вземаме всички мачове за лигата от твоя MatchesRepository
            DataTable dtMatches = _matchesRepo.GetMatchesByLeague(leagueId);
            List<DataRow> playedMatches = new List<DataRow>();

            // Филтрираме само изиграните мачове и натрупваме статистиката
            foreach (DataRow row in dtMatches.Rows)
            {
                if (Convert.ToBoolean(row["is_played"]))
                {
                    playedMatches.Add(row); // Запазваме ги за допълнителния критерий по-долу

                    int homeId = Convert.ToInt32(row["home_team_id"]);
                    int awayId = Convert.ToInt32(row["away_team_id"]);
                    int homeScore = Convert.ToInt32(row["home_score"]);
                    int awayScore = Convert.ToInt32(row["away_score"]);

                    // Проверка дали отборите съществуват в речника (за сигурност)
                    if (!standingsDict.ContainsKey(homeId) || !standingsDict.ContainsKey(awayId)) continue;

                    var homeTeam = standingsDict[homeId];
                    var awayTeam = standingsDict[awayId];

                    // Добавяме изигран мач и голове
                    homeTeam.MatchesPlayed++;
                    awayTeam.MatchesPlayed++;

                    homeTeam.GoalsFor += homeScore;
                    homeTeam.GoalsAgainst += awayScore;

                    awayTeam.GoalsFor += awayScore;
                    awayTeam.GoalsAgainst += homeScore;

                    // Изчисляване на изхода (Победи, Равни, Загуби)
                    if (homeScore > awayScore)
                    {
                        homeTeam.Wins++;
                        awayTeam.Wins++;
                    }
                    else if (homeScore == awayScore)
                    {
                        homeTeam.Draws++;
                        awayTeam.Draws++;
                    }
                    else
                    {
                        homeTeam.Losses++;
                        homeTeam.Wins++;
                    }
                }
            }

            // 3. СОРТИРАНЕ НА КЛАСИРАНЕТО (Включително Директни Срещи за Отлична Оценка)
            List<Standing> standingsList = standingsDict.Values.ToList();

            standingsList.Sort((teamA, teamB) =>
            {
                // Критерий 1: Точки (низходящ ред)
                int compare = teamB.Points.CompareTo(teamA.Points);
                if (compare != 0) return compare;

                // ДОПЪЛНИТЕЛЕН КРИТЕРИЙ: Директни срещи (само ако точките са равни)
                int h2hCompare = GetHeadToHeadResult(teamA.ClubId, teamB.ClubId, playedMatches);
                if (h2hCompare != 0) return h2hCompare; // Връща предимство на победителя от директния мач

                // Критерий 2: Голова разлика (низходящ ред)
                compare = teamB.GoalDifference.CompareTo(teamA.GoalDifference);
                if (compare != 0) return compare;

                // Критерий 3: Отбелязани голове (низходящ ред)
                return teamB.GoalsFor.CompareTo(teamA.GoalsFor);
            });

            return standingsList;
        }

        // Помощен метод за изчисляване на директните срещи между два отбора
        private int GetHeadToHeadResult(int teamAId, int teamBId, List<DataRow> playedMatches)
        {
            int teamAPoints = 0;
            int teamBPoints = 0;

            // Търсим мачове между тези два конкретни отбора
            foreach (var match in playedMatches)
            {
                int hId = Convert.ToInt32(match["home_team_id"]);
                int aId = Convert.ToInt32(match["away_team_id"]);
                int hScore = Convert.ToInt32(match["home_score"]);
                int aScore = Convert.ToInt32(match["away_score"]);

                if (hId == teamAId && aId == teamBId) // TeamA е домакин, TeamB е гост
                {
                    if (hScore > aScore) teamAPoints += 3;
                    else if (hScore == aScore) { teamAPoints += 1; teamBPoints += 1; }
                    else teamBPoints += 3;
                }
                else if (hId == teamBId && aId == teamAId) // TeamB е домакин, TeamA е гост
                {
                    if (hScore > aScore) teamBPoints += 3;
                    else if (hScore == aScore) { teamAPoints += 1; teamBPoints += 1; }
                    else teamAPoints += 3;
                }
            }

            // Връщаме резултат за сортирането: teamB спрямо teamA (за низходящ ред)
            return teamBPoints.CompareTo(teamAPoints);
        }
    }
}