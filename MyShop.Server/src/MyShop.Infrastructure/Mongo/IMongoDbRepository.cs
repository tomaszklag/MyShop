using System;
using System.Threading.Tasks;
using MyShop.Core.Domain;

namespace MyShop.Infrastructure.Mongo
{
    public interface IMongoDbRepository<TEntity> where TEntity : IIdentifiable
    {
        Task<TEntity> GetAsync(Guid id);
        Task AddAsync(TEntity entity);
    }
}