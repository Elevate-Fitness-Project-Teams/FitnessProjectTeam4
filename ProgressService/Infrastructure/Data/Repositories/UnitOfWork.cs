using Microsoft.EntityFrameworkCore.Storage;
using ProgressService.Infrastructure.Data.Contexts;

namespace ProgressService.Infrastructure.Data.Repositories
{
    public class UnitOfWork(ProgressDbContext _context) : IUnitOfWork
    {
        private IDbContextTransaction _transaction;
        private int _depth = 0;
        public async Task<T> ExecuteAsync<T>(Func<Task<T>> action, CancellationToken cancellationToken)
        {
            if (_transaction == null)
                _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            _depth++;
            T result;
            try
            {
                result = await action();
                if (_depth == 1)
                {
                    await _context.SaveChangesAsync(cancellationToken);
                    await _transaction.CommitAsync(cancellationToken);
                }
            }
            catch (Exception)
            {
                if (_transaction != null)
                    await _transaction.RollbackAsync(cancellationToken);
                throw;
            }
            finally
            {
                if (_depth == 1 && _transaction is not null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
                _depth--;
            }
            return result;
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
