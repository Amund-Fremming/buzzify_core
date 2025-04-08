using Domain.Abstractions;

namespace Domain.Entities.Shared;

public sealed class GuestUser : UserBase
{
    public static GuestUser Create() => new() { Guid = Guid.NewGuid(), LastActive = DateTime.Now };
}