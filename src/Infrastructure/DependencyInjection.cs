﻿using Application.Contracts;
using Application.Services;
using Domain.Abstractions;
using Domain.Contracts;
using Infrastructure.Abstractions;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAppDbContext, AppDbContext>();
        services.AddScoped<IAskGameRepository, AskGameRepository>();
        services.AddScoped<ISpinGameRepository, SpinGameRepository>();
        services.AddScoped<IGenericRepository, GenericRepository>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IUserBaseRepository, UserBaseRepository>();

        services.AddDbContext<AppDbContext>(o =>
        {
            var connectionString = configuration["Database:ConnectionString"];
            o.UseNpgsql(connectionString);
        });

        return services;
    }
}