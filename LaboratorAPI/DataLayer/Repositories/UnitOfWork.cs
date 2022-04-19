using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace LaboratorAPI.DataLayer.Repositories
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        Task<bool> SaveChangesAsync();

    }
    public class UnitOfWork : IUnitOfWork
    {


        private readonly DbContext _efDbContext;
        public IUserRepository Users { get; set; }
        
        public UnitOfWork
        (
            EfDbContext efDbContext,
            IUserRepository users
        )
        {
            Users = users;
            _efDbContext = efDbContext;
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                var savedChanges = await _efDbContext.SaveChangesAsync(); 
                return savedChanges > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
