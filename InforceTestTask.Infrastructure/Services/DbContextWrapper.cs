using InforceTestTask.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace InforceTestTask.Infrastructure.Services;

public class DbContextWrapper<T> : IDbContextWrapper<T>
    where T : DbContext
{
    private readonly T _dbContext;

    public DbContextWrapper(
        IDbContextFactory<T> dbContextFactory)
    {
        _dbContext = dbContextFactory.CreateDbContext();
    }

    public T DbContext => _dbContext;

    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
    {
        return _dbContext.Database.BeginTransactionAsync(cancellationToken);
    }
}
