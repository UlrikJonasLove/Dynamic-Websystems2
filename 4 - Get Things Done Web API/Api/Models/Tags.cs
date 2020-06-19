using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Tags
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public bool Done { get; set; }
        public int TodoId { get; set; }
    }
}
