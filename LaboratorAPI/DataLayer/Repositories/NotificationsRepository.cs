using LaboratorAPI.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LaboratorAPI.DataLayer.Repositories
{
    public interface INotificationsRepository : IRepositoryBase<Notification>
    {
        List<Notification> GetAllUntracked();
    }

    public class NotificationsRepository : RepositoryBase<Notification>, INotificationsRepository
    {
        public NotificationsRepository(EfDbContext context) : base(context) { }


        public List<Notification> GetAllUntracked()
        {
            return GetRecords()
                .AsNoTracking()
                .ToList();
        }

    }
}
