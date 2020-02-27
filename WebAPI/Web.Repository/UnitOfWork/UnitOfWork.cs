using System;
using Web.Data.EntityModels;
using Web.Repository.Repositories;

namespace Web.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private WebDataContext _context;
        private bool _disposed = false;

        public UnitOfWork(WebDataContext context)
        {
            _context = context;
            UserRepository = new UserRepository(_context);
            RoleRepository = new GenericRepository<Role>(_context);
        }
        public IUserRepository UserRepository { get; private set; }

        public IGenericRepository<Role> RoleRepository { get; private set; }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
