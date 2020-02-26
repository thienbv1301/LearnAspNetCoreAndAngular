using Microsoft.EntityFrameworkCore;

namespace Web.Data.EntityModels
{
    public class WebDataContext : DbContext
    {
        public WebDataContext(DbContextOptions options)
           : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}