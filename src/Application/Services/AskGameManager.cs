using Application.Contracts;
using Domain.Abstractions;
using Domain.Contracts;
using Domain.DTOs;
using Domain.Entities.Ask;
using Domain.Shared.Enums;
using Domain.Shared.ResultPattern;

namespace Application.Services;

public class AskGameManager(IAskGameRepository gameRepository, IGenericRepository genericRepository) : IAskGameManager
{
    public async Task<Result<AskGame>> CreateGame(int userId, string name, string description = "", Category category = Category.Random)
    {
        var game = AskGame.Create(userId, name, description, category);
        var result = await gameRepository.Create(game);
        if (result.IsError)
        {
            return result.Error;
        }

        game = result.Data;
        game.SetUniversalId();

        return game;
    }

    public async Task<Result<int>> AddQuestion(int gameId, string text)
    {
        var gameResult = await gameRepository.GetById(gameId);
        if (gameResult.IsError || gameResult.IsEmpty)
        {
            return gameResult.Error;
        }

        var question = Question.Create(gameId, text);
        var addResult = gameResult.Data.AddQuestion(question);
        if (addResult.IsError)
        {
            return gameResult.Error;
        }

        var createResult = await genericRepository.Create(question);
        if (createResult.IsError)
        {
            return createResult.Error;
        }

        var updateResult = await gameRepository.Update(gameResult.Data);
        if (updateResult.IsError)
        {
            return gameResult.Error;
        }

        return addResult;
    }

    public async Task<Result<AskGame>> StartGame(int gameId)
    {
        var result = await gameRepository.GetGameWithQuestions(gameId);
        if (result.IsError || result.IsEmpty)
        {
            return result.Error;
        }

        var startResult = result.Data.StartGame();
        return startResult;
    }
}