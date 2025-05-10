using Domain.Shared;
using System.Text.Json.Serialization;

namespace Domain.DTOs;
public sealed record AddedToGameResponse(
    [property: JsonConverter(typeof(JsonStringEnumConverter))] GameType GameType,
    int GameId);