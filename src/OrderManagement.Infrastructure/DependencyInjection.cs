using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderManagement.Application.Abstractions.Clock;
using OrderManagement.Domain.Abstractions;
using OrderManagement.Domain.Orders;
using OrderManagement.Infrastructure.Clock;
using OrderManagement.Infrastructure.Data;
using OrderManagement.Infrastructure.ReadRepository;
using OrderManagement.Infrastructure.Repositories;

namespace OrderManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();

        string connectionString = configuration.GetConnectionString("Database")!;

        services.AddScoped<AuditInterceptor>();

        services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            options.UseSqlServer(connectionString)
                .AddInterceptors(serviceProvider.GetRequiredService<AuditInterceptor>());
        }).AddLogging();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton<SqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));

        AddRepository(services);

        return services;
    }

    private static void AddRepository(IServiceCollection services)
    {
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
        services.AddScoped<IOrderReadRepository, OrderReadRepository>();
    }
}
