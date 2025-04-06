using Domain.Abstractions;
using Domain.Contracts;
using Domain.Entities.Shared;
using Domain.Shared.ResultPattern;
using Microsoft.EntityFrameworkCore;

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

    public Result<List<UserBase>> GetActiveUsersFrom(DateTime dateTime)
    {
        try
        {
            return context.Users.Where(u => u.LastActive < dateTime)
                .ToList();
        }
        catch (Exception ex)
        {
            return new Error(nameof(GetActiveUsersFrom), ex);
        }
    }

    public async Task<Result<List<UserBase>>> GetAll()
    {
        try
        {
            var result = await context.Users.ToListAsync();
            if (result is null)
            {
                return new List<UserBase>();
            }

            return result;
        }
        catch (Exception ex)
        {
            return new Error(nameof(GetAll), ex);
        }
    }
}