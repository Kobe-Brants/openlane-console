using BikeConsole.Core.Mapper.DTO_s;

namespace BikeConsole.BL.Handlers;

public class BikeAuctionPublishedHandler : IBikeAuctionMessageHandler
{
    public bool CanHandle(string messageType) => messageType == "BikeAuction.Published";

    public async Task HandleAsync(BikeAuctionMessage message, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}