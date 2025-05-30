using Application.Contracts;
using Domain.Abstractions;
using Domain.Contracts;
using Domain.DTOs;
using Domain.Entities.Spin;
using Domain.Extentions;
using Domain.Shared.Enums;
using Domain.Shared.ResultPattern;

namespace Application.Services;

public class SpinGameManager(ISpinGameRepository spinGameRepository, IGenericRepository genericRepository) : ISpinGameManager
{
    public async Task<Result<CreateGameResponse>> CreateGame(int userId, string name, Category? category = null)
    {
        var game = SpinGame.Create(name, userId, category);
        var result = await spinGameRepository.Create(game);
        if (result.IsError)
        {
            return result.Error;
        }

        var gameId = result.Data.Id;
        var response = new CreateGameResponse(gameId, int.Parse("2" + gameId));
        return response;
    }

    public async Task<Result<SpinPlayer>> InactivatePlayer(int userId, int gameId)
    {
        var result = await spinGameRepository.GetPlayer(userId, gameId);
        if (result.IsError)
        {
            return result.Error;
        }

        result.Data.SetActive(false);
        var saveResult = await genericRepository.Update(result.Data);
        if (saveResult.IsError)
        {
            return saveResult.Error;
        }

        var gameResult = await spinGameRepository.GetGameWithPlayers(gameId);
        if (gameResult.IsError)
        {
            return gameResult.Error;
        }

        if (gameResult.Data.HostId != userId)
        {
            return new EmptyResult();
        }

        var newHostResult = gameResult.Data.UpdateHost();
        return newHostResult;
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

    public async Task<Result<SpinGame>> CreateGameCopy(int userId, int gameId)
    {
        var result = await spinGameRepository.GetById(gameId);
        if (result.IsError)
        {
            return result.Error;
        }

        var game = result.Data.PartialCopy(userId);
        var saveResult = await spinGameRepository.Create(game);
        return saveResult;
    }

    public async Task<Result<string>> StartRound(int userId, int gameId)
    {
        var gameResult = await spinGameRepository.GetById(gameId);
        if (gameResult.IsError)
        {
            return gameResult.Error;
        }

        var startResult = gameResult.Data.StartRound();
        if (startResult.IsError)
        {
            return startResult.Error;
        }

        var updateResult = await spinGameRepository.Update(gameResult.Data);
        if (updateResult.IsError)
        {
            return updateResult.Error;
        }

        return startResult.Data;
    }

    public async Task<Result<Round>> StartSpin(int userId, int gameId)
    {
        var gameResult = await spinGameRepository.GetById(userId);
        if (gameResult.IsError)
        {
            return gameResult.Error;
        }

        var startResult = gameResult.Data.StartSpin();
        if (startResult.IsError)
        {
            return startResult.Error;
        }

        foreach (var selectedPlayer in startResult.Data.SelectedPlayers)
        {
            await genericRepository.Update(selectedPlayer);
        }

        var updateResult = await spinGameRepository.Update(gameResult.Data);
        if (updateResult.IsError)
        {
            return updateResult.Error;
        }

        return startResult.Data;
    }

    public async Task<Result<int>> AddChallenge(int gameId, int participants, string text, bool readBeforeSpin = false)
    {
        var gameResult = await spinGameRepository.GetById(gameId);
        if (gameResult.IsError)
        {
            return gameResult.Error;
        }

        var challenge = Challenge.Create(gameId, participants, text, readBeforeSpin);
        var addResult = gameResult.Data.AddChallenge(challenge);
        if (addResult.IsError)
        {
            return addResult.Error;
        }

        var createResult = await genericRepository.Create(challenge);
        if (createResult.IsError)
        {
            return createResult.Error;
        }

        var updateResult = await spinGameRepository.Update(gameResult.Data);
        if (updateResult.IsError)
        {
            return updateResult.Error;
        }

        return addResult;
    }
}