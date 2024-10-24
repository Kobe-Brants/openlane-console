using BikeConsole.Core.Mapper.DTO_s.Messages;

namespace BikeConsole.BL.Handlers;

public class BikeAuctionPublishedHandler : BaseAuctionMessageHandler<BikeAuctionPublishedMessage>
{
    public override bool CanHandle(string messageType) => messageType == "BikeAuction.Published";
    public override Task HandleTypedAsync(BikeAuctionPublishedMessage message, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}