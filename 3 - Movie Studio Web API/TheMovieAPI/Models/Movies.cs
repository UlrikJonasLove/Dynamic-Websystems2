using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheMovieAPI.Models
{
    public class Movies
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public int RentedOut { get; set; }
        public bool Available { get; set; }
        public string Trivia { get; set; }
        public List<Ratings> Ratings { get; set; }
        public string RatedBy { get; set; }
       
        public List<Studios> Studios { get; set; }

        public Movies() { }
        public Movies(int id, string name, string description, string trivia, string ratedBy)
        {
            Id = id;
            Name = name;
            Description = description;
            Count = 3;
            Available = true;
            Trivia = trivia;
            RatedBy = ratedBy;
        }
    }
}
