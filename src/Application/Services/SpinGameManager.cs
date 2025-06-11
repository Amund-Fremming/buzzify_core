using Application.Contracts;
using Domain.Abstractions;
using Domain.Contracts;
using Domain.DTOs;
using Domain.Entities.Spin;
using Domain.Extensions;
using Domain.Shared.Enums;
using Domain.Shared.ResultPattern;

namespace Application.Services;

public class SpinGameManager(ISpinGameRepository spinGameRepository, IGenericRepository genericRepository) : ISpinGameManager
{
    public async Task<Result<SpinGame>> CreateGame(int userId, string name, Category? category = null)
    {
        var game = SpinGame.Create(name, userId, category);
        var result = await spinGameRepository.Create(game);
        if (result.IsError)
        {
            return result.Error;
        }

        game = result.Data;
        game.SetUniversalId();
        
        return game;
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
        var result = await spinGameRepository.GetGameWithChallenges(gameId);
        if (result.IsError)
        {
            return result.Error;
        }

        var game = result.Data.PartialCopy(userId);
        var saveResult = await spinGameRepository.Create(game);
        if (saveResult.IsError)
        {
            return result.Error;
        }
        
        game.SetUniversalId();
        return game;
    }

    public async Task<Result<(string, SpinGameState)>> StartRound(int userId, int gameId)
    {
        var gameResult = await spinGameRepository.GetById(gameId);
        if (gameResult.IsError)
        {
            return gameResult.Error;
        }

        var game = gameResult.Data;
        var startResult = game.StartRound();
        if (startResult.IsError)
        {
            return startResult.Error;
        }

        var updateResult = await spinGameRepository.Update(game);
        if (updateResult.IsError)
        {
            return updateResult.Error;
        }

        return (startResult.Data, game.State);
    }

    public async Task<Result<Round>> StartSpin(int userId, int gameId)
    {
        var gameResult = await spinGameRepository.GetGameWithPlayers(gameId);
        if (gameResult.IsError)
        {
            return gameResult.Error;
        }

        var startResult = gameResult.Data.StartSpin();
        if (startResult.IsError)
        {
            return startResult.Error;
        }

        // INFO: Update calls save changes on context and updates the players aswell.
        var updateResult = await spinGameRepository.Update(gameResult.Data);
        if (updateResult.IsError)
        {
            return updateResult.Error;
        }
        
        return startResult.Data;
    }

    public async Task<Result<int>> AddChallenge(int gameId, int participants, string text, bool? readBeforeSpin = null)
    {
        var gameResult = await spinGameRepository.GetById(gameId);
        if (gameResult.IsError)
        {
            return gameResult.Error;
        }

        var challenge = Challenge.Create(gameId, participants, text, readBeforeSpin ?? false);
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

    public async Task<Result<SpinGameState>> CloseChallenges(int userId, int gameId)
    {
        var result = await spinGameRepository.GetGameWithPlayers(gameId);
        if (result.IsError)
        {
            return result.Error;
        }

        var game = result.Data;
        if (game.HostId != userId)
        {
            return new Error("Du har ikke tilgang til denne handlingen.");
        }

        game.CloseChallenges();
        var saveResult = await genericRepository.Update(game);
        if (saveResult.IsError)
        {
            return saveResult.Error;    
        }

        return game.State;
    }
}