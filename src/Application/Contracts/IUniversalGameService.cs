using Domain.DTOs;
using Domain.Shared.ResultPattern;

namespace Application.Contracts;

public interface IUniversalGameService
{
    Task<Result<AddedToGameResult>> AddPlayerToGame(int universalGameId, int userId);
}