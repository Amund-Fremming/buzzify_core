using Domain.Shared;

namespace Domain.DTOs;
public sealed record AddedToGameResult(GameType GameType, int GameId);