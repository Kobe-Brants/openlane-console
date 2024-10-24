using BikeConsole.Core.Mapper.DTO_s;

namespace BikeConsole.BL.Handlers;

public abstract class BaseAuctionMessageHandler<TMessage> : IBikeAuctionMessageHandler<TMessage>
    where TMessage : BaseMessage
{
    public abstract bool CanHandle(string messageType);

    public async Task HandleAsync(BaseMessage message, CancellationToken cancellationToken)
    {
        if (message is TMessage typedMessage)
        {
            await HandleTypedAsync(typedMessage, cancellationToken);
        }
        else
        {
            throw new ArgumentException($"Invalid message type. Expected {typeof(TMessage).Name} but got {message.GetType().Name}");
        }
    }

    public abstract Task HandleTypedAsync(TMessage message, CancellationToken cancellationToken);
}