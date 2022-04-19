using System;
using System.ComponentModel.DataAnnotations;

namespace LaboratorAPI.DataLayer.Entities
{
    public class Notification : BaseEntity
    {
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public Guid UserId { get; set; }

        public AppUser User { get; set; }
    }
}
