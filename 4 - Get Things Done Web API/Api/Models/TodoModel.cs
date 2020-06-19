using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class TodoModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public string Description { get; set; }
        public User Author { get; set; }
        public List<User> Members { get; set; }
    }
}
