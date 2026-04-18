using System;

namespace Football_Manager.Models
{
    public class League
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Season { get; set; }

        public string FullName => $"{Name} ({Season})";
    }
}