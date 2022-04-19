using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace LaboratorAPI.DataLayer.Repositories
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        INotificationsRepository Notifications { get; set; }

        Task<bool> SaveChangesAsync();

    }
    public class UnitOfWork : IUnitOfWork
    {

        private readonly DbContext _efDbContext;
        public IUserRepository Users { get; set; }
        public INotificationsRepository Notifications { get; set; }
        
        public UnitOfWork
        (
            EfDbContext efDbContext,
            IUserRepository users
,           INotificationsRepository notifications)
        {
            Users = users;
            _efDbContext = efDbContext;
            Notifications = notifications;

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
