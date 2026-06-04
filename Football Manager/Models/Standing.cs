namespace Football_Manager.Models
{
    public class Standing
    {
        public int ClubId { get; set; }
        public string ClubName { get; set; }
        public int MatchesPlayed { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }

        // Автоматично изчислявани свойства
        public int GoalDifference => GoalsFor - GoalsAgainst;
        public int Points => (Wins * 3) + Draws;

    }
}