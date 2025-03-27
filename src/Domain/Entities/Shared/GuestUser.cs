using Domain.Abstractions;
using Domain.Shared.TypeScript;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Shared;

public sealed class GuestUser : IUser, ITypeScriptModel
{
    [Key]
    public int Id { get; private set; }

    public Guid Guid { get; private set; }

    public DateTime LastActive { get; private set; }

    public void UpdateActivity() => LastActive = DateTime.Now;

    private GuestUser()
    { }

    public static GuestUser Create() => new() { Guid = Guid.NewGuid(), LastActive = DateTime.Now };
}