using Microsoft.EntityFrameworkCore;

public static class Extensions
{
  public static async Task<IApplicationBuilder> UseMigrations(this IApplicationBuilder app)
  {
    using var scope = app.ApplicationServices.CreateScope();
    using var dbContext = scope.ServiceProvider.GetRequiredService<DiscountDbContext>();
     dbContext.Database.EnsureCreated();
    return app;
  }
}