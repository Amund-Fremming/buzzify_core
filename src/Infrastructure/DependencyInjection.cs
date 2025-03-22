using Domain.Contracts;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddScoped<IAppDbContext, AppDbContext>();
        services.AddScoped<IExampleRepository, ExampleRepository>();

        return services;
    }
}