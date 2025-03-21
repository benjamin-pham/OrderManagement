using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using OrderManagement.API.Middleware;
using OrderManagement.Application;
using OrderManagement.Infrastructure;
using Serilog;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddControllers();

builder.Services.AddFluentValidationRulesToSwagger();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    var dir = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory));
    foreach (var fi in dir.EnumerateFiles("*.xml"))
    {
        options.IncludeXmlComments(fi.FullName);
    }
});

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseMiddleware<RequestContextLoggingMiddleware>();

app.UseSerilogRequestLogging();

app.UseRouting();

#pragma warning disable ASP0014 // Suggest using top level route registrations
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
#pragma warning restore ASP0014 // Suggest using top level route registrations

app.Run(async (context) =>
{
    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
    await context.Response.WriteAsync("404 - Resource Not Found");
});

await app.RunAsync();
