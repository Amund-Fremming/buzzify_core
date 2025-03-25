using Domain.Shared.TypeScript;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Shared;

public class Player : ITypeScriptModel
{
    [Key]
    public int Id { get; }

    public string? Hash { get; private set; }

    public static Player Create => new() { Hash = Guid.NewGuid().ToString() };
    public static Player Empty => new();
}