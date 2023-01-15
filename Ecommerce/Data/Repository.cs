using Ecommerce.Data;
using Ecommerce.Models;
using System.Linq.Expressions;

public class Repository <TEntity> : IRepository<TEntity> where TEntity : class
{
    protected ApplicationDbContext applicationDbContext;
    public Repository(ApplicationDbContext dbContext)
    {
        applicationDbContext = dbContext;
    }
    public bool Add(TEntity entity)
    {

        applicationDbContext.Set<TEntity>().Add(entity);
        applicationDbContext.SaveChanges();

        return true;

    }
    public bool AddAll(IEnumerable<TEntity> entity)
    {

        applicationDbContext.Set<TEntity>().AddRange(entity);
        applicationDbContext.SaveChanges();

        return true;

    }
    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
    {

        return applicationDbContext.Set<TEntity>().Where(predicate);


    }
    public TEntity? Get(int id)
    {


        return applicationDbContext.Set<TEntity>().Find(id);

    }

    public IEnumerable<TEntity> GetAll()
    {
        return applicationDbContext.Set<TEntity>().ToList();
    }

    public bool Remove(TEntity entity)
    {
        applicationDbContext.Set<TEntity>().Remove(entity);
        applicationDbContext.SaveChanges();
        return true;

    }
}