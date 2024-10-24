using BikeConsole.Core.Mapper.DTO_s.ResourceData;

namespace BikeConsole.Core.Mapper.DTO_s.Messages;

public class BikeAuctionExtendedMessage : BaseMessage
{
    public BikeAuctionExtendedData ResourceData { get; set; }
}