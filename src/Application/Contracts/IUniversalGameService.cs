using Domain.DTOs;
using Domain.Shared.ResultPattern;

namespace Application.Contracts;

public interface IUniversalGameService
{
    Task<Result<AddedToGameResponse>> AddPlayerToGame(int userId, int universalGameId);
}