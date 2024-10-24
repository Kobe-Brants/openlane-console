using BikeConsole.Core.Mapper.DTO_s.ResourceData;

namespace BikeConsole.Core.Mapper.DTO_s.Messages;

public class BikeAuctionRemovedMessage : BaseMessage
{
    public BikeAuctionRemovedData ResourceData { get; set; }
}