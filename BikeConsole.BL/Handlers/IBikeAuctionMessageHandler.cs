using BikeConsole.Core.Mapper.DTO_s;

namespace BikeConsole.BL.Handlers;

public interface IBikeAuctionMessageHandler
{
    Task HandleAsync(BikeAuctionMessage message, CancellationToken cancellationToken);
    bool CanHandle(string messageType);
}