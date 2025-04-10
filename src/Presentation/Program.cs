using Application;
using Domain.Entities.Hub;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Presentation.Sockets;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddEndpointsApiExplorer();
services.AddControllers();
services.AddSignalR();
services.AddLogging();

services
    .AddInfrastructure()
    .AddApplication();

services.AddResponseCompression(o =>
{
    o.EnableForHttps = true;
});

services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "B_ API", Version = "v1" });
});

services.AddHttpClient(nameof(BeerPrice), client =>
{
    var baseUrlString = builder.Configuration["BeerPrices:Url"] ?? throw new InvalidOperationException("BeerPrices url does not exist.");
    client.BaseAddress = new Uri(baseUrlString);
});

services.AddDbContext<AppDbContext>(o =>
{
    var connectionString = builder.Configuration.GetConnectionString("Database:ConnectionString");
    o.UseNpgsql(connectionString);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.MapHubs();
app.MapControllers();
app.UseHttpsRedirection();

app.Run();