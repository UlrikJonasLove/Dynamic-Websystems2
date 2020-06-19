using System.Collections.Generic;

namespace NasaWebAPI.Models
{
    public interface IRoverRepository
    {
        IEnumerable<Rovers> Rovers { get; }

        Rovers GetRover(int id);
        
    } 
}
