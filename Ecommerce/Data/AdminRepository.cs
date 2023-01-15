using Microsoft.EntityFrameworkCore;
using Ecommerce.Models;
using System.Linq.Expressions;
namespace Ecommerce.Data
{
    public interface IAdminRepository : IRepository<Admin>
    {
      public bool Authenticate(String email,String password);
    }
    public class AdminRepository : Repository<Admin>, IAdminRepository
    {
        public AdminRepository(ApplicationDbContext dbContext) : base(dbContext) { }
        public bool Authenticate(String email,String password)
        {
            try
            {
                return Find((admin) => admin.Email == email && admin.Password == password).Count() > 0;
            }
            catch(Exception) {
                return false; 
            }
        }
    }
}

