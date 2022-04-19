using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LaboratorAPI.DataLayer.Entities
{
    public class AppUser : BaseEntity
    {
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public List<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
