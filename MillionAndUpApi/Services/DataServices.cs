using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using MillionAndUpApi.Interfaces.Services;
using System.Linq.Expressions;

namespace MillionAndUpApi.Services
{
    public class DataServices : IDataServices
    {
        private readonly ApplicationDbContext _context;

        public DataServices(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TEntity>> GetAll<TEntity>() where TEntity : class
        {
            var result = await _context.Set<TEntity>().ToListAsync();
            return result;
        }

        public async Task<List<TEntity>> GetAllByFiler<TEntity>(Func<TEntity, bool> entity) where TEntity : class
        {
            if (entity == null) return new List<TEntity>();
            var result = _context.Set<TEntity>().Where(entity).ToList();
            return result;
        }


        public async Task<TEntity> GetById<TEntity>(Guid Id) where TEntity : class
        {
            var result = await _context.Set<TEntity>().FindAsync(Id);
            return result;
        }

        public async Task<TEntity> Create<TEntity>(TEntity entity) where TEntity : class
        {
            var result = _context.Set<TEntity>().Add(entity).Entity;
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<TEntity> Update<TEntity>(TEntity entity) where TEntity : class
        {
            var result = _context.Set<TEntity>().Update(entity).Entity;
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task Delete<TEntity>(TEntity entity) where TEntity : class
        {

            var result = _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidExists<TEntity>(Expression<Func<TEntity, bool>> func) where TEntity : class
        {
            var result = await _context.Set<TEntity>().AnyAsync(func);
            return result;
        }
    }
}
