using Microsoft.EntityFrameworkCore.Storage;
using ProfileService.Infrastructure.Data;

namespace ProfileService.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProfileDbContext _context;
        private int _depth = 0;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(ProfileDbContext context)
        {
            _context = context;
        }

        public async Task ExecuteAsync(Func<Task> action)
        {
            if (_depth == 0 && _transaction == null)
            {
                _transaction = await _context.Database.BeginTransactionAsync();
            }

            _depth++;

            try
            {
                await action();

                _depth--;
                if (_depth == 0)
                {
                    await _context.SaveChangesAsync();
                    if (_transaction != null)
                    {
                        await _transaction.CommitAsync();
                    }
                }
            }
            catch
            {
                _depth--;

                if (_depth == 0 && _transaction != null)
                {
                    await _transaction.RollbackAsync();
                }
                throw;
            }
            finally
            {
                if (_depth == 0 && _transaction != null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
        }
    }
}
