using Microsoft.EntityFrameworkCore;
using Ecommerce.Models;
using System.Linq.Expressions;
namespace Ecommerce.Data
{
    public interface ICommandItemRepository : IRepository<CommandItem>
    {
    }
    public class CommandItemRepository : Repository<CommandItem>, ICommandItemRepository
    {
        public CommandItemRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}

