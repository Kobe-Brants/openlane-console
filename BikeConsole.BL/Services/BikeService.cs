using BikeConsole.BL.Handlers;
using BikeConsole.Core.Mapper.DTO_s;

namespace BikeConsole.BL.Services;

public class BikeService : IBikeService
{
    private readonly IEnumerable<IBikeAuctionMessageHandler> _handlers;

    public BikeService(IEnumerable<IBikeAuctionMessageHandler> handlers)
    {
        _handlers = handlers;
    }

    public async Task ProcessMessageAsync(BaseMessage message, CancellationToken cancellationToken)
    {
        var handler = _handlers.FirstOrDefault(h => h.CanHandle(message.Type))
                      ?? throw new ArgumentException($"No handler found for message type: {message.Type}");

        await handler.HandleAsync(message, cancellationToken);
    }
}