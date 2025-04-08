using Domain.Abstractions;
using Domain.Shared.ResultPattern;

namespace Application.Contracts;

public interface IUserService
{
    Task<Result> UpdateUserActivity(int userId);

    Task<Result<UserBase>> CreateGuestUser();

    Task<Result<UserBase>> CreateRegisteredUser(string name, string email, string password);

    Task<Result<bool>> DoesPlayerExist(int userId);
}