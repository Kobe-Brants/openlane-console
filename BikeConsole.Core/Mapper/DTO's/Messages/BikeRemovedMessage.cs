using BikeConsole.Core.Mapper.DTO_s.ResourceData;

namespace BikeConsole.Core.Mapper.DTO_s.Messages;

public class BikeRemovedMessage : BaseMessage
{
    public BikeRemovedData ResourceData { get; set; }
}