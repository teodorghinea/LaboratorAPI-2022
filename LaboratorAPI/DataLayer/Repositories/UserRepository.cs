using LaboratorAPI.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LaboratorAPI.DataLayer.Repositories
{

    public interface IUserRepository : IRepositoryBase<User>
    {
        User GetUserByName(string name);
    }

    public class UserRepository : RepositoryBase<User>, IUserRepository
    {

        public UserRepository(EfDbContext context) : base(context) { }


        public User GetUserByName(string name)
        {
            //SELECT TOP(1) * from Users as u
            var result = GetRecords()

                 // INNER JOIN Notifications as n on n.UserId = u.Id
                .Include(u => u.Notifications)

                 // WHERE u.FirstName = @name or u.LastName = @name
                .Where(u => u.FirstName == name || u.LastName == name)
                // IQueryable pana aici -> rezultatul nu e concret
                .FirstOrDefault();  
                // -> rezultat concret
            return result;
        }

    }
}
