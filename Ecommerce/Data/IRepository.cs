using System.Linq.Expressions;

public interface IRepository<TEntity> where TEntity : class
{
    TEntity? Get(int id);
    IEnumerable<TEntity> GetAll();
    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    bool Add(TEntity entity);
    bool AddAll(IEnumerable<TEntity> entity);
    bool Remove(TEntity entity);
}