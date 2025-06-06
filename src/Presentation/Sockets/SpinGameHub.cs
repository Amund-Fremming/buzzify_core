using Application.Contracts;
using Domain.Contracts;
using Domain.Entities.Spin;
using Microsoft.AspNetCore.SignalR;
using Presentation.Constants;

namespace Presentation.Sockets;

public class SpinGameHub(ISpinGameManager manager, ISpinGameRepository repository) : Hub
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
        var iterations = Clients.Caller.SendAsync(HubChannels.Iterations, result.Data.Iterations);
        
        await Task.WhenAll(add, iterations);
    }

    public async Task AddChallenge(int gameId, int participants, string text, bool? readBeforeSpin = null)
    {
        var result = await manager.AddChallenge(gameId, participants, text, readBeforeSpin);
        if (result.IsError)
        {
            await Clients.Caller.SendAsync(HubChannels.Error, result.Error);
            return;
        }

        await Clients.Group(gameId.ToString()).SendAsync(HubChannels.Iterations, result.Data);
    }
    
    public async Task CloseChallenges(int userId, int gameId)
    {
        var result = await manager.CloseChallenges(userId, gameId);
        if (result.IsError)
        {
            await Clients.Caller.SendAsync(HubChannels.Error, result.Error.Message);
            return;
        }

        await Clients.Group(gameId.ToString()).SendAsync(HubChannels.State, result.Data);
    }

    public async Task StartRound(int userId, int gameId)
    {
        var result = await manager.StartRound(userId, gameId);
        if (result.IsError)
        {
            await Clients.Caller.SendAsync(HubChannels.Error, result.Error);    
            return;
        }

        var (message, state) = result.Data;
        await Clients.Caller.SendAsync(HubChannels.Message, message);
        await Clients.GroupExcept(gameId.ToString(), Context.ConnectionId).SendAsync(HubChannels.State, state);
    }
    
    public async Task StartSpin(int userId, int gameId)
    {
        var result = await manager.StartSpin(userId, gameId);
        if (result.IsError)
        {
            await Clients.Caller.SendAsync(HubChannels.Error, result.Error);    
            return;
        }

        var round = result.Data;
        for (var i = 0; i < 10; i++)
        {
            var indexes = GetRandomNumbers(round.RoundParticipants, round.AllPlayers.Count);
            var spinTasks = new List<Task>();
            foreach (var index in indexes)
            {
                var playerId = round.AllPlayers[index].Id;
                spinTasks.Add(Clients.Group(gameId.ToString()).SendAsync(HubChannels.Game, playerId));
            }
            
            await Task.WhenAll(spinTasks);  
            Thread.Sleep(300);
        }
        
        var tasks = round.SelectedPlayers.Select(player => Clients.Group(gameId.ToString()).SendAsync(HubChannels.Game, player.Id)).ToList();
        tasks.Add(Clients.Group(gameId.ToString()).SendAsync(HubChannels.Message, round.ChallengeText));
        
        await Task.WhenAll(tasks);  
    }

    private static List<int> GetRandomNumbers(int participants, int numPlayers)
    {
        var rnd = new Random();
        var i = rnd.Next(0, numPlayers - 1);
        
        var ids = new HashSet<int>();
        while (ids.Count != participants)
        {
            var j = rnd.Next(0, numPlayers - 1);
            ids.Add(j);
        }

        return ids.ToList();
    }
}