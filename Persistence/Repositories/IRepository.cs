using System.Linq.Expressions;

namespace Persistence.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    //for finding objects
    Task<TEntity> Get(int id);
    Task<IEnumerable<TEntity>> GetAll();
    Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);

    //for adding objects
    Task Add(TEntity entity);
    void AddRange(IEnumerable<TEntity> entities);

    //for removing objects
    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
    
    //for updating objects
    void Update(TEntity entity);

}