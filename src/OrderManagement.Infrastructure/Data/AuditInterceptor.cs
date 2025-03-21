using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OrderManagement.Application.Abstractions.Clock;
using OrderManagement.Domain.Abstractions;

namespace OrderManagement.Infrastructure.Data;
internal class AuditInterceptor : SaveChangesInterceptor
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public AuditInterceptor(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
       DbContextEventData eventData,
       InterceptionResult<int> result,
       CancellationToken cancellationToken = new CancellationToken())
    {
        if (eventData.Context is not null)
        {
            var entities = eventData.Context
                .ChangeTracker
                .Entries<IAuditable>()
                .Where(e =>
                    e.State == EntityState.Modified
                    || e.State == EntityState.Added)
                .ToList();

            for (int i = 0; i < entities.Count; i++)
            {
                if (entities[i].State == EntityState.Added)
                {
                    entities[i].Entity.CreatedAt = _dateTimeProvider.UtcNow;
                }

                if (entities[i].State == EntityState.Modified)
                {
                    entities[i].Entity.UpdatedAt = _dateTimeProvider.UtcNow;
                }
            }
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
