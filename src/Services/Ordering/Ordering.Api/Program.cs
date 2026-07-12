var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApiServices();


var app = builder.Build();

app.UseApiServices();

if (app.Environment.IsDevelopment())
{
  await app.InitializeDatabaseAsync();
}

app.Run();

