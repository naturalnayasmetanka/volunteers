﻿using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using Shared.Core.Abstractions.Database;
using Volunteers.Infrastructure.Contexts;

namespace Volunteers.Infrastructure;

public class VolunteersUnitOfWork : IUnitOfWork
{
    private readonly VolunteersDbContext _context;
    public VolunteersUnitOfWork(VolunteersDbContext context)
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
