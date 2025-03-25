using Domain.Shared.TypeScript;

namespace Domain.Entities.Shared;

public sealed record CreateGameRequest : ITypeScriptModel
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
}