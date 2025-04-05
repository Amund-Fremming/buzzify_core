using Domain.Entities.Shared;
using System.ComponentModel.DataAnnotations;

namespace Domain.Abstractions;

public abstract class UserBase : IUser
{
    [Key]
    public int Id { get; set; }

    public Guid Guid { get; set; }

    public DateTime LastActive { get; set; }

    public IList<Vote> Votes { get; set; } = [];

    public void UpdateActivity() => LastActive = DateTime.Now;
}