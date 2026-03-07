using System;
using System.Collections.Generic;
using System.Text;

namespace Football_Manager
{
    public class Player
    {
        public int Id { get; set; }
        public int ClubId { get; set; }
        public string ClubName { get; set; } // Ще ни трябва за визуализация в таблицата
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Position { get; set; }
        public int ShirtNumber { get; set; }
    }
}
