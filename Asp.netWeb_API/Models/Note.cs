using System.ComponentModel.DataAnnotations;

namespace Asp.netWeb_API.Models
{
    public class Note
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Body is required")]
        public string Body { get; set; }
        public DateTime DateStamp { get; set; }
        public string? Image { get; set; }
    }
}
