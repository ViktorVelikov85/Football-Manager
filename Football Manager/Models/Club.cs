using System;
using System.Collections.Generic;
using System.Text;

namespace Football_Manager.Models
{
    public class Club
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Stadium { get; set; }
        public string FoundedYear { get; set; }
    }
}
