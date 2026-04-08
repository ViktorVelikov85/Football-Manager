using System;

namespace Football_Manager.Models
{
    public class Player
    {
        public int Id { get; set; }
        public int ClubId { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Position { get; set; }
        public int ShirtNumber { get; set; }
        public string Status { get; set; } 
    }
}