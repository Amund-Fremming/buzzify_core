using Domain.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Shared;

public sealed class User : IUser
{
    [Key]
    public int Id { get; private set; }
    public Guid Guid { get; private set; }
    public DateTime LastActive { get; private set; }

    public void UpdateActivity() => LastActive = DateTime.Now;

    public static User Create() => new() { Guid = Guid.NewGuid(), LastActive = DateTime.Now };
}