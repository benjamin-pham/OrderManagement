using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderManagement.Application.Abstractions.Clock;
using OrderManagement.Domain.Abstractions;
using OrderManagement.Infrastructure.Clock;
using OrderManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();

        string connectionString = configuration.GetConnectionString("Database")!;

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton<SqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));

        AddRepository(services);

        return services;
    }

    private static void AddRepository(IServiceCollection services)
    {

    }
}
