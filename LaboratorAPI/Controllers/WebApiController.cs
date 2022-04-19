using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace LaboratorAPI.Controllers
{
    public class WebApiController : ControllerBase
    {
        [NonAction]
        protected Guid? GetUserId()
        {
            string userIdClaimValue = User
                  .Claims
                  .FirstOrDefault(x => x.Type == "userId")?
                  .Value;

            bool succeeded = Guid.TryParse(userIdClaimValue, out Guid userId);

            return succeeded ? userId : throw new Exception("No user Id");
        }
    }
}
