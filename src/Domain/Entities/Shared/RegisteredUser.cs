using Domain.Abstractions;
using Domain.Shared.TypeScript;

namespace Domain.Entities.Shared;

public sealed class RegisteredUser : UserBase, ITypeScriptModel
{
    public static RegisteredUser Create() => new() { Guid = Guid.NewGuid(), LastActive = DateTime.Now };
}