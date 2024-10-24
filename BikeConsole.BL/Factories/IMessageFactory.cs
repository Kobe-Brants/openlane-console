using BikeConsole.Core.Mapper.DTO_s;

namespace BikeConsole.BL.Factories;

public interface IMessageFactory
{
    public BaseMessage? CreateMessage(string json);
}