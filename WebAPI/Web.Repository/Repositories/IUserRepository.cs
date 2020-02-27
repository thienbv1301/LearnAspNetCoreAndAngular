using Web.Data.EntityModels;

namespace Web.Repository.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User GetUserByName(string name);
        User GetUserByAccount(string accName);
    }
}
