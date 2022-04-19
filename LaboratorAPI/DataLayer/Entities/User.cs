using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LaboratorAPI.DataLayer.Entities
{
    public class User : BaseEntity
    {
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }


        // relatia one-to-many  : one User has many Notifications
        public List<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
