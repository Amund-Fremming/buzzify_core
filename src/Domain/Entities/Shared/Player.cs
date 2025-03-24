using Domain.Shared.TypeScript;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Shared;

public class Player : ITypeScriptModel
{
    [Key]
    public int Id { get; set; }

    public string? Hash { get; set; }

    public static Player Create => new() { Hash = Guid.NewGuid().ToString() };
    public static Player Empty => new();
}