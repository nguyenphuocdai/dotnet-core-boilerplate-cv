using AspNetCoreHero.Application.Interfaces.Repositories;
using AspNetCoreHero.Infrastructure.Persistence.Contexts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _dbContext;
        private bool _disposed;

        public UnitOfWork(ApplicationContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<int> Commit(CancellationToken cancellationToken)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public Task Rollback()
        {
            //todo
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    //dispose managed resources
                    _dbContext.Dispose();
                }
            }
            //dispose unmanaged resources
            _disposed = true;
        }
    }
}
