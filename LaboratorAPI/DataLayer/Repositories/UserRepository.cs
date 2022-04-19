using LaboratorAPI.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LaboratorAPI.DataLayer.Repositories
{

    public interface IUserRepository : IRepositoryBase<AppUser>
    {
        AppUser GetUserByEmail(string name);
    }

    public class UserRepository : RepositoryBase<AppUser>, IUserRepository
    {

        public UserRepository(EfDbContext context) : base(context) { }


        public AppUser GetUserByEmail(string email)
        {
            //SELECT TOP(1) * from Users as u
            var result = GetRecords()

                 // INNER JOIN Notifications as n on n.UserId = u.Id
                .Include(u => u.Notifications)

                 // WHERE u.Email = @email 
                // IQueryable pana aici -> rezultatul nu e concret
                .Where(u => u.Email == email)
                
                .FirstOrDefault();  
                // -> rezultat concret
            return result;
        }
    }
}
