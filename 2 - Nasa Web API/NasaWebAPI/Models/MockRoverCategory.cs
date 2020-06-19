using System.Collections.Generic;
using System.Linq;

namespace NasaWebAPI.Models
{
    public class MockRoverCategory : IRoverCategory
    {
        public IEnumerable<RoverCategory> AllCategories =>
            new List<RoverCategory>
            {
                new RoverCategory{CategoryId = 1, CategoryName = "The First Rover"},
                new RoverCategory{CategoryId = 2, CategoryName = "The Second Rover"},
                new RoverCategory{CategoryId = 3, CategoryName = "The Third Rover"}
            };

        public RoverCategory GetCategory(int id)
        {
            return AllCategories.FirstOrDefault(c => c.CategoryId == id);
        }
    }
}
