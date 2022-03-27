using System.Linq.Expressions;

namespace Persistence.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    //for finding objects
    TEntity Get(int id);
    IEnumerable<TEntity> GetAll();
    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

    //for adding objects
    void Add(TEntity entity);
    void AddRange(IEnumerable<TEntity> entities);

    //for removing objects
    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);

}