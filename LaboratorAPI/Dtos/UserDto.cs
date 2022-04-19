using System.Collections.Generic;

namespace LaboratorAPI.Dtos
{
    public class UserDto : LightUserDto
    {
        public string Email { get; set; }

        public List<NotificationDto> Notifications { get; set; }

    }

    public class NotificationDto 
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }

}
