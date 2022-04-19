using System.ComponentModel.DataAnnotations;

namespace LaboratorAPI.Dtos
{
    public class UserDto
    {
        [Required(ErrorMessage = "First-name required!")]   
        
        [MaxLength(50, ErrorMessage = "First-name too long!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last-name required!")]

        [MaxLength(50, ErrorMessage = "Last-name too long!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email required!")]

        [MaxLength(100, ErrorMessage = "Email too long!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password required!")]

        [MinLength(4, ErrorMessage = "Password must be at least 4 characters long!")]
        public string Password { get; set; }
    }
}
