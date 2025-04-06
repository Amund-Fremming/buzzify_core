using Application.Contracts;
using Application.Services;
using Domain.Abstractions;
using Domain.Contracts;
using Infrastructure.Abstractions;
using Infrastructure.External;
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
        services.AddScoped<IGenericRepository, GenericRepository>();
        services.AddScoped<IBeerPriceClient, BeerPriceClient>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IUserBaseRepository, UserBaseRepository>();

        return services;
    }
}