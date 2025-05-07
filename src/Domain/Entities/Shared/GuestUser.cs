using Domain.Abstractions;

namespace Domain.Entities.Shared;

public sealed class GuestUser : UserBase
{
    private GuestUser()
    { }

    public static GuestUser Create() => new() { Guid = Guid.NewGuid(), LastActive = DateTime.UtcNow };
}