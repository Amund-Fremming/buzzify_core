using Application.Contracts;
using Domain.Abstractions;
using Domain.Contracts;
using Domain.Entities.Spin;
using Domain.Shared.Enums;
using Domain.Shared.ResultPattern;

namespace Application.Services;

public class SpinGameManager(ISpinGameRepository spinGameRepository, IGenericRepository genericRepository) : ISpinGameManager
{
    public async Task<Result<int>> CreateGame(int userId, string name, Category? category = null)
    {
        var game = SpinGame.Create(name, userId, category);
        var result = await spinGameRepository.Create(game);
        if (result.IsError)
        {
            return result.Error;
        }

        return result.Data.Id;
    }

    public async Task<Result> InactivatePlayer(int userId, int gameId)
    {
        var result = await spinGameRepository.GetPlayer(userId, gameId);
        if (result.IsError)
        {
            return result.Error;
        }

        var player = result.Data;
        player.SetActive(true);

        var saveResult = await genericRepository.Update(player);
        return saveResult;
    }

    public async Task<Result> JoinGame(int userId, int gameId)
    {
        var result = await spinGameRepository.GetPlayer(userId, gameId);
        if (result.IsError)
        {
            return result.Error;
        }

        if (!result.IsEmpty)
        {
            result.Data.SetActive(true);
            var updateResult = await genericRepository.Update(result.Data);
            return updateResult;
        }

        var player = SpinPlayer.Create(userId, gameId);
        var createResult = await genericRepository.Create(player);
        return createResult;
    }

    public Task<Result<SpinGame>> StartExistingGame(int userId, int gameId) => throw new NotImplementedException();

    public Task<Result<SpinGame>> StartGame(int userId, int gameId) => throw new NotImplementedException();

    public Task<string> StartRound(int userId, int gameId) => throw new NotImplementedException();

    public Task<Challenge> StartSpin(int userId, int gameId) => throw new NotImplementedException();

    public Task<Result<SpinPlayer>> UpdateHost(int userId, int gameId) => throw new NotImplementedException();

    public Task<Result> AddChallenge(int participants, string text, bool readBeforeSpin = false) => throw new NotImplementedException();
}