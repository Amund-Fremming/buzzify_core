using Application.Contracts;
using Domain.Contracts;
using Domain.Entities.Ask;
using Domain.Shared.Enums;
using Domain.Shared.ResultPattern;

namespace Application.Services;

public class AskGameManager(IAskGameRepository gameRepository) : IAskGameManager
{
    public async Task<Result<int>> CreateGame(int userId, string name, string? description = null, Category? category = null)
    {
        var game = AskGame.Create(userId, name, description, category);
        var result = await gameRepository.Create(game);
        if (result.IsError)
        {
            return result.Error;
        }

        return result.Data.Id;
    }

    public async Task<Result> AddQuestion(int gameId, string text)
    {
        var result = await gameRepository.GetById(gameId);
        if (result.IsError || result.IsEmpty)
        {
            return result.Error;
        }

        var question = Question.Create(gameId, text);
        var addResult = result.Data.AddQuestion(question);
        if (addResult.IsError)
        {
            return result.Error;
        }

        var updateResult = await gameRepository.Update(result.Data);
        return updateResult;
    }

    public async Task<Result<AskGame>> StartGame(int gameId)
    {
        var result = await gameRepository.GetById(gameId);
        if (result.IsError || result.IsEmpty)
        {
            return result.Error;
        }

        var startResult = result.Data.StartGame();
        return startResult;
    }
}