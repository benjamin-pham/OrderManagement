using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using OrderManagement.API.Middleware;
using OrderManagement.Application;
using OrderManagement.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddControllers();

builder.Services.AddFluentValidationRulesToSwagger();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseMiddleware<RequestContextLoggingMiddleware>();

app.UseSerilogRequestLogging();

app.MapControllers();

await app.RunAsync();
