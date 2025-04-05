using Domain.Shared.ResultPattern;

namespace Application.Contracts;

public interface IUserService
{
    Task<Result> UpdateUserActivity(int userId);
}