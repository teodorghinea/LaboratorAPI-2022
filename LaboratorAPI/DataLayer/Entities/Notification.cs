using System;
using System.ComponentModel.DataAnnotations;

namespace LaboratorAPI.DataLayer.Entities
{
    // mosteneste BaseEntity pentru a folosi proprietatile comune (Id, ...)
    public class Notification : BaseEntity
    {
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
