using Domain.Abstractions;
using Domain.Contracts;
using Domain.Entities.Shared;
using Domain.Shared.ResultPattern;
using Infrastructure.Abstractions;

namespace Infrastructure.Repositories;

public class UserBaseRepository(IAppDbContext context) : RepositoryBase<UserBase>(context), IUserBaseRepository
{
    public Task<Result<GuestUser>> CreateGuestUser(GuestUser user) => throw new NotImplementedException();

    public Task<Result<RegisteredUser>> CreateRegisteredUser(RegisteredUser user) => throw new NotImplementedException();
}