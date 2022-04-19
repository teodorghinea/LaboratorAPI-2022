using LaboratorAPI.DataLayer.Entities;
using LaboratorAPI.DataLayer.Repositories;
using LaboratorAPI.Dtos;
using LaboratorAPI.Services;
using Microsoft.AspNetCore.Authorization;
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
        private readonly ICustomerAuthService _authorization;

        public UserController(IUnitOfWork unitOfWork, ICustomerAuthService authorization)
        {
            _unitOfWork = unitOfWork;
            _authorization = authorization;
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult<bool>> Add([FromBody] UserDto request)
        {
            if(request == null)
            {
                BadRequest(error: "Request must not be empty!");
            }

            var hashedPassword = _authorization.HashPassword(request.Password);

            var user = new AppUser()
            {
                FirstName = request.FirstName,
                LastName  = request.LastName,
                PasswordHash = hashedPassword,
                Email = request.Email,
            };

            _unitOfWork.Users.Insert(user);
            var saveResult = await _unitOfWork.SaveChangesAsync();

            return Ok(saveResult);
        }

        [HttpPost]
        [Route("login")]
        public ActionResult<ResponseLogin> Login([FromBody] RequestLogin request)
        {
            var user = _unitOfWork.Users.GetUserByEmail(request.Email);
            if (user == null) return BadRequest("User not found!");

            var samePassword = _authorization.VerifyHashedPassword(user.PasswordHash, request.Password);
            if(!samePassword) return BadRequest("Invalid password!");

            var user_jsonWebToken = _authorization.GetToken(user);
            
            return Ok(new ResponseLogin
            {
                Token = user_jsonWebToken
            });
        }


        [HttpGet]
        [Route("all")]
        [Authorize()]
        public ActionResult<List<AppUser>> GetAll()
        {
            var users = _unitOfWork.Users.GetAll(includeDeleted: false).ToList();
            return Ok(users);
        }


        [HttpGet]
        [Route("delete/all")]
        public async Task<ActionResult<List<AppUser>>> DeleteAll()
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
        public async Task<ActionResult<bool>> Update([FromRoute] Guid userId)
        {
           var user = _unitOfWork.Users.GetById(userId);

            user.FirstName = "First Name 2";

            _unitOfWork.Users.Update(user);

            return await _unitOfWork.SaveChangesAsync();
        }
    }
}
