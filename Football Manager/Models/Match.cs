using System;
using System.Collections.Generic;
using System.Text;

namespace Football_Manager.Models
{
    public class Match
    {
        public int Id { get; set; }
        public int LeagueId { get; set; }
        public int RoundNo { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }

        // Полета за резултат (ще ни трябват в следващия етап)
        public int? HomeScore { get; set; }
        public int? AwayScore { get; set; }

        public DateTime? MatchDate { get; set; }
        public bool IsPlayed { get; set; }

        // Помощни свойства за показване на имената в таблицата
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
    }
}
