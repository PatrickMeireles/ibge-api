using Ibge.Infrastructure.Data.Mapping.Base;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Ibge.Infrastructure.Data.Context;

[ExcludeFromCodeCoverage]
public class DatabaseContext : DbContext, IContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options, bool applyMigration = false) : base(options)
    {
        if (applyMigration && Database.IsRelational())
            Database.Migrate();

        Database.EnsureCreated();
    }

    public DatabaseContext() : base()
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IEFMappingEntrypoint).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public virtual async Task<bool> CommitAsync(CancellationToken cancellationToken = default) =>
        await SaveChangesAsync(cancellationToken) > 0;
}
