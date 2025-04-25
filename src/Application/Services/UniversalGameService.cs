using Application.Contracts;
using Domain.Abstractions;
using Domain.Contracts;
using Domain.DTOs;
using Domain.Entities.Ask;
using Domain.Shared.ResultPattern;

namespace Application.Services;

public class UniversalGameService(IAskGameManager askGameManager, ISpinGameRepository spinGameRepository, IGenericRepository genericRepository) : IUniversalGameService
{
    public async Task<Result<AddedToGameResult>> AddPlayerToGame(int universalGameId, int userId)
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

        if (indicator is 1)
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

            return new AddedToGameResult(nameof(AskGame), gameId);
        }

        if (indicator is 2)
        {
            var spinGameResult = await spinGameRepository.GetGameWithPlayers(userId);
            if (spinGameResult.IsError)
            {
                return spinGameResult.Error;
            }

            // FORTSETT her
            var addResult = spinGameResult.Data.AddPlayer();
        }
    }

    private static (int, int) ExtractGameIndicator(int universalId)
    {
        // INFO: buffer length 10, because its max int32 value
        Span<char> buffer = stackalloc char[10];
        var len = universalId.TryFormat(buffer, out int charsWritten) ? charsWritten : 0;
        var indicator = buffer[0] - '0';
        var gameId = int.Parse(buffer[..1]);
        return (indicator, gameId);
    }
}