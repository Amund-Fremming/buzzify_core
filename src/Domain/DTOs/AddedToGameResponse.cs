using Domain.Shared;
using System.Text.Json.Serialization;
using Domain.Abstractions;

namespace Domain.DTOs;
public sealed record AddedToGameResponse(
    [property: JsonConverter(typeof(JsonStringEnumConverter))] GameType GameType,
    GameBase GameBase);