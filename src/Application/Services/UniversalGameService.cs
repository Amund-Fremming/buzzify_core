using Application.Contracts;
using Domain.Abstractions;
using Domain.DTOs;
using Domain.Entities.Ask;
using Domain.Entities.Spin;
using Domain.Shared;
using Domain.Shared.ResultPattern;

namespace Application.Services;

public class UniversalGameService(ISpinGameManager spinGameManager, IGenericRepository genericRepository) : IUniversalGameService
{
    public async Task<Result<AddedToGameResponse>> AddPlayerToGame(int userId, int universalGameId)
    {
        if (universalGameId < 10)
        {
            return new Error("Universal spill id kan ikke være mindre enn 10.");
        }

        var userResult = await genericRepository.GetById<UserBase>(userId);
        if (userResult.IsError)
        {
            return userResult.Error;
        }

        var (indicator, gameId) = ExtractGameIndicator(universalGameId);

        var result = indicator switch
        {
            1 => await AddPlayerToAskGame(gameId),
            2 => await AddPlayerToSpinGame(gameId, userId),
            _ => new Error("Klarte ikke finne riktig spill.")
        };

        return result;
    }

    private async Task<Result<AddedToGameResponse>> AddPlayerToSpinGame(int userId, int gameId)
    {
        var result = await genericRepository.GetById<SpinGame>(gameId);
        if (result.IsError)
        {
            return result.Error;
        }

        var joinResult = await spinGameManager.JoinGame(userId, gameId);
        if (joinResult.IsError)
        {
            return joinResult.Error;
        }

        return new AddedToGameResponse(GameType.SpinGame, gameId);
    }

    private async Task<Result<AddedToGameResponse>> AddPlayerToAskGame(int gameId)
    {
        var askGameResult = await genericRepository.GetById<AskGame>(gameId);
        if (askGameResult.IsError)
        {
            return askGameResult.Error;
        }

        if (askGameResult.Data.State == AskGameState.Closed)
        {
            return new Error("Det er ikke mulig å bli med i dette spillet.");
        }

        return new AddedToGameResponse(GameType.AskGame, gameId);
    }

    private static (int, int) ExtractGameIndicator(int universalId)
    {
        Span<char> buffer = stackalloc char[10];
        var len = universalId.TryFormat(buffer, out int charsWritten) ? charsWritten : 0;
        var indicator = buffer[0] - '0';
        var gameId = int.Parse(buffer[1..]);
        return (indicator, gameId);
    }
}