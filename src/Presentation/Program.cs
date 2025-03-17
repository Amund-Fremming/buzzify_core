using Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
var services = builder.Services;

services.AddApplicationLayer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();