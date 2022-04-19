using LaboratorAPI.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LaboratorAPI.DataLayer.Repositories
{

    public interface IRepositoryBase<T> where T : BaseEntity
    {
        IList<T> GetAll(bool asNoTracking = false, bool includeDeleted = false);
        T GetById(Guid id, bool asNoTracking = false);
        void Insert(T record);
        void Update(T record);
        void Delete(T record);
    }

    public class RepositoryBase<T> : IDisposable, IRepositoryBase<T> where T : BaseEntity, new()
    {

        private readonly DbContext _db;
        private readonly DbSet<T> _dbSet;

        protected RepositoryBase(EfDbContext context)
        {
            _db = context;
            _dbSet = context.Set<T>();
        }

        public IList<T> GetAll(bool asNoTracking = false, bool includeDeleted = false)
        {
            var query = includeDeleted
               ? _dbSet
               : _dbSet.Where(entity => entity.DeletedAt == null);

            return asNoTracking
                ? query.AsNoTracking().ToList()
                : query.ToList();
        }

        public T GetById(Guid id, bool asNoTracking = false)
        {
            return GetRecords(asNoTracking).FirstOrDefault(e => e.Id == id);
        }

        public virtual void Insert(T record)
        {
            if (_db.Entry(record).State == EntityState.Detached)
            {
                _db.Attach(record);
                _db.Entry(record).State = EntityState.Added;
            }
        }

        public virtual void Update(T record)
        {
            if (_db.Entry(record).State == EntityState.Detached)
                _db.Attach(record);

            _db.Entry(record).State = EntityState.Modified;
        }

        public void Delete(T record)
        {
            if (record != null)
            {
                record.DeletedAt = DateTime.UtcNow;
                Update(record);
            }
        }

        public void Dispose()
        {
            _db?.Dispose();
        }

        protected IQueryable<T> GetRecords(bool asNoTracking = false, bool includeDeleted = false)
        {
            var result = includeDeleted ?
                _dbSet.Where(e => e.DeletedAt != null)
                :
                _dbSet;

            return asNoTracking ? result.AsNoTracking() : result;
        }
    }
}
