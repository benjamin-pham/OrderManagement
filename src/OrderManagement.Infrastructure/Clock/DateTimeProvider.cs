using OrderManagement.Application.Abstractions.Clock;

namespace OrderManagement.Infrastructure.Clock;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
