using BikeConsole.Core.Mapper.DTO_s.ResourceData;

namespace BikeConsole.Core.Mapper.DTO_s.Messages;

public class BikeAuctionWithdrawnMessage : BaseMessage
{
    public BikeAuctionWithdrawnData ResourceData { get; set; }
}