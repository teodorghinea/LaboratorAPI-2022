using System.ComponentModel.DataAnnotations;

namespace LaboratorAPI.Dtos
{
    public class UserDto
    {
        [MaxLength(50, ErrorMessage = "First-name too long!")]
        public string FirstName { get; set; }

        [MaxLength(50, ErrorMessage = "Last-name too long!")]
        public string LastName { get; set; }

    }
}
