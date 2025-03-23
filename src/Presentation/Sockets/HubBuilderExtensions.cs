namespace Presentation.Sockets;

public static class HubBuilderExtensions
{
    public static WebApplication MapHubs(this WebApplication app)
    {
        app.MapHub<AskGameHub>("/hubs/ask-game");
        app.MapHub<SpinGameHub>("/hubs/spin-game");

        return app;
    }
}