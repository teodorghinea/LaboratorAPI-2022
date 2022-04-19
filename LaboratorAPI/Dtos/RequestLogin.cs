using System.ComponentModel.DataAnnotations;

namespace LaboratorAPI.Dtos
{
    public class RequestLogin
    {
        [Required(ErrorMessage = "Email required!")]
        [MaxLength(100, ErrorMessage = "Email too long!")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password required!")]
        [MinLength(4, ErrorMessage = "Password must be at least 4 characters long!")]
        public string Password { get; set; }
    }
}
