using LaboratorAPI.DataLayer.Entities;
using LaboratorAPI.DataLayer.Repositories;
using LaboratorAPI.Dtos;
using LaboratorAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        public async Task<ActionResult<bool>> Add([FromBody] RegisterUserDto request)
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
        [Authorize(Roles = "User")]
        public ActionResult<List<LightUserDto>> GetAll()
        {
            var users = _unitOfWork.Users.GetAll(includeDeleted: false).Select(u => new LightUserDto
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
            });
            return Ok(users);
        }

        [HttpGet]
        [Route("my-account")]
        [Authorize(Roles = "User")]
        public ActionResult<bool> MyAccount()
        {
            var userId = GetUserId();
            if(userId == null) return Unauthorized();

            var user = _unitOfWork.Users.GetById((Guid)userId);

            return Ok(new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            });
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

        [NonAction]
        private Guid? GetUserId()
        {
            string userIdClaimValue = User
                  .Claims
                  .FirstOrDefault(x => x.Type == "userId")?
                  .Value;

            bool succeeded = Guid.TryParse(userIdClaimValue, out Guid userId);

            return succeeded ? userId : null;
        }
    }
}
