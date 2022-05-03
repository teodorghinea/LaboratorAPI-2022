using LaboratorAPI.DataLayer.Entities;
using LaboratorAPI.DataLayer.Repositories;
using LaboratorAPI.Dtos;
using LaboratorAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace LaboratorAPI.Controllers
{
    [ApiController]
    [Route("api/notifications")]
    public class NotificationController : WebApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        public NotificationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpPost]
        [Route("add")]
        [Authorize]
        public async Task<ActionResult> AddNotification([FromBody][Required] PostNotification request)
        {
            if (request == null) return BadRequest("Empty notification");
            
            var user = _unitOfWork.Users.GetById((Guid)GetUserId());
            if (user == null) return BadRequest();

            var newNotification = new Notification
            {
                Title = request.Title,
                Description = request.Description,
                UserId = user.Id,
                Picture = request.Picture
            };

            _unitOfWork.Notifications.Insert(newNotification);
            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }


    }
}
