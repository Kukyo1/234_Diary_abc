using Asp.netWeb_API.Models;
using System.ComponentModel.DataAnnotations;

namespace Asp.netWeb_API.ViewModels
{
    public class UserVM
    {
        [Required(ErrorMessage = "Email field is required")]
        [EmailAddress(ErrorMessage = "Wrong email format")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
