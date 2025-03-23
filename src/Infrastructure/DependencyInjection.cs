using Domain.Contracts;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IAppDbContext, AppDbContext>();
        services.AddScoped<IAskGameRepository, AskGameRepository>();
        services.AddScoped<ISpinGameRepository, SpinGameRepository>();

        return services;
    }
}