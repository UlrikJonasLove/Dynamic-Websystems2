using System;
using System.Collections.Generic;
using System.Linq;

namespace NasaWebAPI.Models
{
    public class MockRoverRepository : IRoverRepository
    {
        private readonly IRoverCategory _roverCategory = new MockRoverCategory();
        public IEnumerable<Rovers> Rovers =>
            new List<Rovers>
            {
                new Rovers {RoverId = 1, Speed = 1100, Weight = "1000kg", Wheels = 4, Date = DateTime.Now.AddDays(-3), Category = _roverCategory.GetCategory(1)},
                new Rovers {RoverId = 2, Speed = 2000, Weight = "1500kg", Wheels = 5, Date = DateTime.Now.AddDays(-2), Category = _roverCategory.GetCategory(2)},
                new Rovers {RoverId = 3, Speed = 1010, Weight = "800kg", Wheels = 4, Date = DateTime.Now, Category = _roverCategory.GetCategory(3)}
            };

        public Rovers GetRover(int id)
        {
            return Rovers.FirstOrDefault(r => r.RoverId == id);
        }
    }
}
