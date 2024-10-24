using BikeConsole.Core.Mapper.DTO_s.ResourceData;

namespace BikeConsole.Core.Mapper.DTO_s.Messages;

public class BikeUpdatedMessage : BaseMessage
{
    public BikeUpdatedData ResourceData { get; set; }
}