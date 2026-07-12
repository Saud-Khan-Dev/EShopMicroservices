using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
  public static IServiceCollection AddApplicationServices(this IServiceCollection services)
  {
    services.AddMediatR(cf =>
    {
      cf.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());

    });
    return services;
  }
}