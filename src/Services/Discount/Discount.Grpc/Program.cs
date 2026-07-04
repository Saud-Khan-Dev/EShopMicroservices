using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

builder.Services.AddDbContext<DiscountDbContext>(opt =>
{
  opt.UseSqlite(builder.Configuration.GetConnectionString("Database"));
});

var app = builder.Build();

app.UseMigrations();

app.MapGrpcService<DiscountService>();

app.Run();
