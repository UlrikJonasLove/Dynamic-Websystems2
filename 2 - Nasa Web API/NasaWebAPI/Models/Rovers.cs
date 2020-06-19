using System;

namespace NasaWebAPI.Models
{
    public class Rovers
    {
        public int RoverId { get; set; }
        public int Speed { get; set; }
        public int Wheels { get; set; }
        public string Weight { get; set; }
        public DateTime Date { get; set; }
        public RoverCategory Category { get; set; }
    }
}
