using Domain.Shared.Enums;

namespace Domain.DTOs;

public sealed record CreateSpinGameRequest(int UserId, string Name, Category? Category = null);