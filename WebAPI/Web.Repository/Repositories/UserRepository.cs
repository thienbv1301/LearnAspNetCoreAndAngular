using Microsoft.EntityFrameworkCore;
using System.Linq;
using Web.Data.EntityModels;

namespace Web.Repository.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(WebDataContext context): base(context)
        {

        }
        public User GetUserByName(string name)
        {
            return _context.Users.Include(a=>a.Account).Include(r=>r.Role).FirstOrDefault(u => u.Name == name);
        }
    }
}
