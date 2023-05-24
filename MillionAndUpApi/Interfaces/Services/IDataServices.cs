using Data.Entities;
using System.Linq.Expressions;

namespace MillionAndUpApi.Interfaces.Services
{
    public interface IDataServices
    {
        Task<List<TEntity>> GetAll<TEntity>() where TEntity : class;
        Task<List<TEntity>> GetAllByFiler<TEntity>(Func<TEntity, bool> entity) where TEntity : class;
        Task<TEntity> GetById<TEntity>(Guid Id) where TEntity : class;
        Task<TEntity> Create<TEntity>(TEntity entity) where TEntity : class;
        Task<TEntity> Update<TEntity>(TEntity entity) where TEntity : class;
        Task Delete<TEntity>(TEntity entity) where TEntity : class;
        Task SaveChanges();
        Task<bool> ValidExists<TEntity>(Expression<Func<TEntity, bool>> func) where TEntity : class;
    }
}
