using System;
using Web.Data.EntityModels;
using Web.Repository.Repositories;

namespace Web.Repository.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository  UserRepository { get; }
        IGenericRepository<Role> RoleRepository { get; }
        void Save();

    }
}
