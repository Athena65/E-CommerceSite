using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.DTO
{
    public class UserForRegistrationDto
    {
        [Required(ErrorMessage ="Email is required.")]
        [EmailAddress]
        public string? Email { get; set; }
        [Required(ErrorMessage ="Password is required!")]
        [DataType(DataType.Password)]   
        public string? Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="The password must be same!")]
        public string? ConfirmPassword { get; set; }
    }
}
