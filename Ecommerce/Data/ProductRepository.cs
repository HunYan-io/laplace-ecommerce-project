using Microsoft.EntityFrameworkCore;
using Ecommerce.Models;
using System.Linq.Expressions;
namespace Ecommerce.Data
{
    public interface IProductRepository: IRepository<Product>
    {
        IEnumerable<Product> GetProductsForPage(int page); 
        IEnumerable<Product> GetByIds(IEnumerable<int> ids);
        int GetPageCount();
        IEnumerable<Product> GetLatest();
    }
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext) { }
        public IEnumerable<Product> GetProductsForPage(int page)
        {
            return (applicationDbContext.Set<Product>().Skip((page - 1) * 9).Take(9).ToList()); 
        }

        public IEnumerable<Product> GetByIds(IEnumerable<int> ids)
        {
            return applicationDbContext.Set<Product>().Where(product => ids.Contains(product.Id));
        }

        public int GetPageCount()
        {
            var fraction = ((double) applicationDbContext.Set<Product>().Count()) / 9;
            return (int) Math.Ceiling(fraction);
        }
        public IEnumerable<Product> GetLatest() { 
            return applicationDbContext.Set<Product>().OrderByDescending(product => product.Id).Take(3).ToList();
        }
    }
}

