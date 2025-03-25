using Domain.Shared.TypeScript;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Shared;

public sealed class Player : ITypeScriptModel
{
    [Key]
    public int Id { get; private set; }

    public string? Hash { get; private set; }

    public static Player Create() => new() { Hash = Guid.NewGuid().ToString() };

    public static Player Empty => new() { Id = 0 };
}