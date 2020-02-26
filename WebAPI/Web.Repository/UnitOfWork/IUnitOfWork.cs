using System;
using System.Threading.Tasks;
using Web.Data.EntityModels;
using Web.Repository.Repositories;

namespace Web.Repository.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository  UserRepository { get; }
        IGenericRepository<Account> AccountRepository { get; }
        IGenericRepository<Role> RoleRepository { get; }
        Task SaveAsync();

    }
}
