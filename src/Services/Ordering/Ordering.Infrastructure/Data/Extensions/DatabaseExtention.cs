using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class DatabaseExtention
{
  public static async Task InitializeDatabaseAsync(this WebApplication app)
  {
    var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.MigrateAsync().GetAwaiter().GetResult();

    await InitialData.SeedCustomerAsync(context);
    await InitialData.SeedProductAsync(context);
    await InitialData.SeedOrderAndOrderItemAsync(context);
  }

} 