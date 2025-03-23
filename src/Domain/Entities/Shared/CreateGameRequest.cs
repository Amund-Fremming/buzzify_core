namespace Domain.Entities.Shared;

public record CreateGameRequest
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
}