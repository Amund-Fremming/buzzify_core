using Application.Contracts;
using Domain.Abstractions;
using Domain.Shared.ResultPattern;

namespace Application.Services;

internal class UserService(IGenericRepository genericRepository) : IUserService
{
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
}