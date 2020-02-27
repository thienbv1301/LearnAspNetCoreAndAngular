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

        public User GetUserByAccount(string accName)
        {
            return _context.Users.FirstOrDefault(u => u.Account.Trim().ToUpper() == accName.Trim().ToUpper());
        }

        public User GetUserByName(string name)
        {
            return _context.Users.Include(r=>r.Role).FirstOrDefault(u => u.Name == name);
        }
    }
}
