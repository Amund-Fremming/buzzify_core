using Application;
using Infrastructure;
using Presentation.Sockets;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddEndpointsApiExplorer();
services.AddControllers();
services.AddSignalR();
services.AddLogging();

services
    .AddInfrastructure(builder.Configuration)
    .AddApplication();

services.AddResponseCompression(o =>
{
    o.EnableForHttps = true;
});

services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "B_ API", Version = "v1" });
});

var app = builder.Build();

app.UseCors(policy =>
{
    policy.AllowAnyOrigin()
          .AllowAnyHeader()
          .AllowAnyMethod();
});

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