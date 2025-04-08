using Domain.Shared.Enums;

namespace Domain.DTOs;
public sealed record CreateAskGameRequest
{
    public int UserId { get; set; }
    public string GameName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Category? Category { get; set; } = null;
}