using Application.Contracts;
using Domain.Contracts;
using Microsoft.AspNetCore.SignalR;
using Presentation.Constants;

namespace Presentation.Sockets;

public class AskGameHub(IAskGameManager manager, IAskGameRepository repository) : Hub
{
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var query = Context.GetHttpContext()?.Request.Query;
        if (query is null)
        {
            await Clients.Caller.SendAsync(HubChannels.Error, "HttpContext er ikke gyldig.");
            return;
        }

        if (query[HubConstants.GameId].FirstOrDefault() is not string gameId)
        {
            await Clients.Caller.SendAsync(HubChannels.Error, "Game id er ikke gyldig.");
            return;
        }

        var remove = Groups.RemoveFromGroupAsync(Context.ConnectionId, gameId.ToString());
        var confirm = Clients.Caller.SendAsync(HubChannels.Message, "Du har forlatt spillet.");

        await Task.WhenAll(remove, confirm);
    }

    public override async Task OnConnectedAsync()
    {
        var query = Context.GetHttpContext()?.Request.Query;
        if (query is null)
        {
            await Clients.Caller.SendAsync(HubChannels.Error, "HttpContext er ikke gyldig.");
            return;
        }

        if (query[HubConstants.GameId].FirstOrDefault() is not string gameId)
        {
            await Clients.Caller.SendAsync(HubChannels.Error, "Game id er ikke gyldig.");
            return;
        }

        if (!int.TryParse(gameId, out var gameIdInt))
        {
            await Clients.Caller.SendAsync(HubChannels.Error, "Spill id må være ett tall.");
            return;
        }

        var result = await repository.GetById(gameIdInt);
        if (result.IsError)
        {
            await Clients.Caller.SendAsync(HubChannels.Error, result.Error);
            return;
        }

        var add = Groups.AddToGroupAsync(Context.ConnectionId, gameId);
        var message = Clients.Caller.SendAsync(HubChannels.Message, "Du er med, start å legge inn spørsmål!");
        var iterations = Clients.Caller.SendAsync(HubChannels.Iterations, result.Data.Iterations);

        await Task.WhenAll(add, message, iterations);
    }

    public async Task AddQuestion(int gameId, string question)
    {
        var result = await manager.AddQuestion(gameId, question);
        if (result.IsError)
        {
            await Clients.Group(gameId.ToString()).SendAsync(HubChannels.Error, result.Message);
            return;
        }

        await Clients.Group(gameId.ToString()).SendAsync(HubChannels.Iterations, result.Data);
    }

    public async Task StartGame(int gameId)
    {
        var result = await manager.StartGame(gameId);
        if (result.IsError)
        {
            await Clients.Caller.SendAsync(HubChannels.Error, result.Error);
            return;
        }

        await Clients.GroupExcept(gameId.ToString(), Context.ConnectionId).SendAsync(HubChannels.State, result.Data.State);
        await Clients.Caller.SendAsync(HubChannels.Game, result.Data);
    }
}