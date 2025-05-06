using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using Shared.Core.Abstractions.Database;
using Species.Infrastructure.Contexts;

namespace Species.Infrastructure;

public class SpeciesUnitOfWork : IUnitOfWork
{
    private readonly SpeciesDbContext _context;
    public SpeciesUnitOfWork(SpeciesDbContext context)
    {
        _context = context;
    }

    public async Task<IDbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        return transaction.GetDbTransaction();
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
