using LaboratorAPI.DataLayer.Entities;
using LaboratorAPI.DataLayer.Repositories;
using LaboratorAPI.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaboratorAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
       
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult<bool>> Add([FromBody] UserDto request)
        {
            if(request == null)
            {
                BadRequest(error: "Request must not be empty!");
            }

            var user = new User()
            {
                FirstName = request.FirstName,
                LastName  = request.LastName,
            };

            _unitOfWork.Users.Insert(user);

            var saveResult = await _unitOfWork.SaveChangesAsync();
            
            return Ok(saveResult);
        }

        [HttpGet]
        [Route("all")]
        public ActionResult<List<User>> GetAll()
        {
            var users = _unitOfWork.Users.GetAll(includeDeleted: false).ToList();
            return Ok(users);
        }


        [HttpGet]
        [Route("delete/all")]
        public async Task<ActionResult<List<User>>> DeleteAll()
        {
            var users = _unitOfWork.Users.GetAll().ToList();

            foreach(var user in users)
            {
                _unitOfWork.Users.Delete(user);
            }
            await _unitOfWork.SaveChangesAsync();
            return Ok(users);
        }

        [HttpPost]
        [Route("update/{userId}")]
        public async Task<bool> Update([FromRoute] Guid userId)
        {
           var user = _unitOfWork.Users.GetById(userId);

            user.FirstName = "First Name 2";

            _unitOfWork.Users.Update(user);

            return await _unitOfWork.SaveChangesAsync();
        }
    }
}
