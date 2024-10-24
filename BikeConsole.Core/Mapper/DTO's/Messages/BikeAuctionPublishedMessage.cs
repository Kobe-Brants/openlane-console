using BikeConsole.Core.Mapper.DTO_s.ResourceData;

namespace BikeConsole.Core.Mapper.DTO_s.Messages;

public class BikeAuctionPublishedMessage : BaseMessage
{
    public BikeAuctionPublishedData ResourceData { get; set; }
}