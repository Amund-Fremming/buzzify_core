using Application.Contracts;
using Microsoft.AspNetCore.SignalR;
using Presentation.Constants;

namespace Presentation.Sockets;

public class AskGameHub(IAskGameManager manager) : Hub
{
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        if (Context.Items[HubConstants.GameId] is not int gameId)
        {
            await Clients.Caller.SendAsync(HubChannels.Error, "Game id is not valid or present. Game not left properly.");
            return;
        }

        var remove = Groups.RemoveFromGroupAsync(Context.ConnectionId, gameId.ToString());
        var confirm = Clients.Caller.SendAsync(HubChannels.Message, "Du har forlatt spillet.");
        var alert = Clients.Group(gameId.ToString()).SendAsync(HubChannels.PlayerLeft, "En bruker forlot spillet.");

        await Task.WhenAll(remove, confirm, alert);
    }

    public override async Task OnConnectedAsync()
    {
        if (Context.Items[HubConstants.GameId] is not int gameId)
        {
            await Clients.Caller.SendAsync(HubChannels.Error, "Game id is not valid or present.");
            return;
        }

        await Groups.AddToGroupAsync(gameId.ToString(), Context.ConnectionId);
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
        => await Clients.Group(gameId.ToString())
            .SendAsync(HubChannels.State, "Legg vekk telefonen, spillmesteren har startet spillet.");
}