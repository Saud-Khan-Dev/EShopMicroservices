using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
  {
    var connecctionString = configuration.GetConnectionString("Database");

    services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptors>();
    services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventInterceptor>();


    services.AddDbContext<ApplicationDbContext>((sp, option) =>
    {
      option.AddInterceptors(sp.GetService<ISaveChangesInterceptor>()!);

      option.UseSqlServer(connecctionString);
    });
    return services;
  }
}