using Microsoft.EntityFrameworkCore;
using Ecommerce.Models;
using System.Linq.Expressions;
namespace Ecommerce.Data
{
    public interface ICommandRepository : IRepository<Command>
    {
    }
    public class CommandRepository : Repository<Command>, ICommandRepository
    {
        public CommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public new IEnumerable<Command> GetAll()
        {
            return applicationDbContext.Set<Command>().Include(c => c.Items).ToList();
        }
    }
    
}

