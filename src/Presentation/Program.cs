using Application;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Presentation.Sockets;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddOpenApi();
services.AddEndpointsApiExplorer();
services.AddControllers();
services.AddSignalR();
services.AddLogging();
services.AddResponseCompression(o => o.EnableForHttps = true);

services
    .AddInfrastructure()
    .AddApplication();

services.AddHttpClient(nameof(BeerPrice), client =>
{
    var baseUrlString = builder.Configuration["BeerPrices:Url"] ?? throw new InvalidOperationException("BeerPrices url does not exist.");
    client.BaseAddress = new Uri(baseUrlString);
});

services.AddDbContext<AppDbContext>(o =>
{
    var connectionString = builder.Configuration.GetConnectionString("Database:ConnectionString");
    if (string.IsNullOrEmpty(connectionString))
    {
        o.UseInMemoryDatabase("InMemoryDb");
    }
    else
    {
        o.UseNpgsql(connectionString);
    }
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapHubs();
app.MapControllers();
app.UseHttpsRedirection();

app.Run();