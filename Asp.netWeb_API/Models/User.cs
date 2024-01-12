using System.ComponentModel.DataAnnotations;

namespace Asp.netWeb_API.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name field is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email field is required")]
        [EmailAddress(ErrorMessage = "Wrong email format")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public List<Note>? Notes { get; set; }
    }
}
