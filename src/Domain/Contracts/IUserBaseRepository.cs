using Domain.Abstractions;
using Domain.Entities.Shared;
using Domain.Shared.ResultPattern;

namespace Domain.Contracts;

public interface IUserBaseRepository
{
    Task<Result<RegisteredUser>> CreateRegisteredUser(RegisteredUser user);

    Task<Result<RegisteredUser>> UpdateRegisteredUser(RegisteredUser user);

    Task<Result<GuestUser>> CreateGuestUser(GuestUser user);

    Task<Result<List<UserBase>>> GetAll();
}