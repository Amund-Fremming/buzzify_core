using Application.Contracts;
using Microsoft.AspNetCore.SignalR;
using Presentation.Constants;

namespace Presentation.Sockets;

public class AskGameHub(IAskGameManager manager) : Hub
{
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var query = Context.GetHttpContext()?.Request.Query;
        if (query is null)
        {
            await Clients.Caller.SendAsync(HubChannels.Error, "HttpContext is not valid.");
            return;
        }

        if (query[HubConstants.GameId].FirstOrDefault() is not string gameId)
        {
            await Clients.Caller.SendAsync(HubChannels.Error, "Game id is not valid or present.");
            return;
        }

        var remove = Groups.RemoveFromGroupAsync(Context.ConnectionId, gameId.ToString());
        var confirm = Clients.Caller.SendAsync(HubChannels.Message, "Du har forlatt spillet.");
        var alert = Clients.GroupExcept(gameId.ToString(), Context.ConnectionId).SendAsync(HubChannels.PlayersLeft, "En bruker forlot spillet.");

        await Task.WhenAll(remove, confirm, alert);
    }

    public override async Task OnConnectedAsync()
    {
        var query = Context.GetHttpContext()?.Request.Query;
        if (query is null)
        {
            await Clients.Caller.SendAsync(HubChannels.Error, "HttpContext is not valid.");
            return;
        }

        if (query[HubConstants.GameId].FirstOrDefault() is not string gameId)
        {
            await Clients.Caller.SendAsync(HubChannels.Error, "Game id is not valid or present.");
            return;
        }

        await Groups.AddToGroupAsync(gameId, Context.ConnectionId);
        await Clients.Caller.SendAsync(HubChannels.Message, "Du er med, nå er det bare å legge inn spørsmål!");
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
            await Clients.Caller.SendAsync(HubChannels.Error, "En feil skjedde ved oppstart av spillet.");
            return;
        }

        await Clients.Group(gameId.ToString()).SendAsync(HubChannels.State, result.Data.State);
    }
}