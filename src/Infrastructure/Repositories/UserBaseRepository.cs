﻿using Domain.Contracts;
using Domain.Entities.Shared;
using Domain.Shared.ResultPattern;

namespace Infrastructure.Repositories;

public class UserBaseRepository(IAppDbContext context) : IUserBaseRepository
{
    public async Task<Result<GuestUser>> CreateGuestUser(GuestUser user)
    {
        try
        {
            await context.Users.AddAsync(user);
            return user;
        }
        catch (Exception ex)
        {
            return new Error(nameof(CreateRegisteredUser), ex);
        }
    }

    public async Task<Result<RegisteredUser>> CreateRegisteredUser(RegisteredUser user)
    {
        try
        {
            await context.Users.AddAsync(user);
            return user;
        }
        catch (Exception ex)
        {
            return new Error(nameof(CreateRegisteredUser), ex);
        }
    }

    public async Task<Result<RegisteredUser>> UpdateRegisteredUser(RegisteredUser user)
    {
        try
        {
            context.Users.Update(user);
            await context.SaveChangesAsync();
            return user;
        }
        catch (Exception ex)
        {
            return new Error(nameof(CreateRegisteredUser), ex);
        }
    }
}