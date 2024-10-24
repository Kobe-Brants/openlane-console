using BikeConsole.Core.Mapper.DTO_s.ResourceData;

namespace BikeConsole.Core.Mapper.DTO_s.Messages;

public class BikeAuctionCreatedMessage : BaseMessage
{
    public BikeAuctionCreatedData ResourceData { get; set; }
}