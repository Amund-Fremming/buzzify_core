using Microsoft.AspNetCore.SignalR;

namespace Presentation.Sockets;

public class SpinGameHub : Hub
{
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await Task.Run(() => Console.WriteLine("Disconnnected"));
    }

    public override async Task OnConnectedAsync()
    {
        await Task.Run(() => Console.WriteLine("Connnected"));
    }
}