using Application.Contracts;
using Domain.Abstractions;
using Domain.Entities.Shared;
using Domain.Shared.ResultPattern;

namespace Application.Services;

internal class UserService(IGenericRepository genericRepository) : IUserService
{
    public async Task<Result<UserBase>> CreateGuestUser()
    {
        var user = GuestUser.Create();
        var result = await genericRepository.Create(user);
        if (result.IsError)
        {
            return result.Error;
        }

        return user;
    }

    public async Task<Result<UserBase>> CreateRegisteredUser(string name, string email, string password)
    {
        // TODO: Register user with Auth0

        var NOT_IMPLEMENTED = true;
        if (NOT_IMPLEMENTED)
            throw new NotImplementedException();

        var authId = "IMPLEMENT_THIS";
        var user = RegisteredUser.Create(authId, name, email);
        var result = await genericRepository.Create(user);
        if (result.IsError)
        {
            return result.Error;
        }

        return user;
    }

    public async Task<Result> UpdateUserActivity(int userId)
    {
        var result = await genericRepository.GetById<UserBase>(userId);
        if (result.IsError || result.IsEmpty)
        {
            return result.Error ?? new Error("Cannot update non-existing user.");
        }

        result.Data.UpdateActivity();
        var updateResult = await genericRepository.Update(result.Data);
        return updateResult;
    }

    public async Task<Result<bool>> DoesUserExist(int userId)
    {
        var result = await genericRepository.GetById<UserBase>(userId);
        if (result.IsError)
        {
            return result.Error;
        }

        return !result.IsEmpty;
    }
}