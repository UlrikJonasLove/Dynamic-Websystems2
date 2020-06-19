using System.Collections.Generic;

namespace NasaWebAPI.Models
{
    interface IRoverCategory
    {
        IEnumerable<RoverCategory> AllCategories { get; }

        RoverCategory GetCategory(int id);
    }
}
