using Domain.Entities.Ask;
using Domain.Entities.Spin;

namespace Presentation.Sockets;

public static class HubBuilderExtensions
{
    public static WebApplication MapHubs(this WebApplication app)
    {
        app.MapHub<AskGameHub>($"/hub/v1/{nameof(AskGame)}");
        app.MapHub<SpinGameHub>($"/hub/v1/{nameof(SpinGame)}");

        return app;
    }
}