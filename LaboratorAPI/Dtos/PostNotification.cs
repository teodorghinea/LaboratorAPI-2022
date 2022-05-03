using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LaboratorAPI.Dtos
{
    public class PostNotification 
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        public string Picture { get; set; }

    }
}
