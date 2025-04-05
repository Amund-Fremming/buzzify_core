using Domain.Abstractions;
using Domain.Shared.TypeScript;

namespace Domain.Entities.Shared;

public sealed class GuestUser : UserBase, ITypeScriptModel
{
    public static GuestUser Create() => new() { Guid = Guid.NewGuid(), LastActive = DateTime.Now };
}