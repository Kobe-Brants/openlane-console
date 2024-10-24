using BikeConsole.Core.Mapper.DTO_s;

namespace BikeConsole.BL.Handlers;

public interface IBikeAuctionMessageHandler
{
    Task HandleAsync(BaseMessage message, CancellationToken cancellationToken);
    bool CanHandle(string messageType);
}

public interface IBikeAuctionMessageHandler<TMessage> : IBikeAuctionMessageHandler 
    where TMessage : BaseMessage
{
    Task HandleTypedAsync(TMessage message, CancellationToken cancellationToken);
}