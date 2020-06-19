using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheMovieAPI.Models
{
    public class Studios
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public List<Ratings> Ratings { get; set; }
   

        public Studios()
        {

        }

        public Studios(int id, string name, string location)
        {
            Id = id;
            Name = name;
            Location = location;
        }
    }
}
