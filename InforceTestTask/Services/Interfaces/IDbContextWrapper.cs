using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace InforceTestTask.Services.Interfaces;

public interface IDbContextWrapper<T>
     where T : DbContext
{
     T DbContext { get; }
     Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
}