namespace BikeConsole.Core.Mapper.DTO_s;

public abstract class BaseMessage
{
    public required string MessageId { get; set; }

    public required string Type { get; set; }

    public DateTime TimestampUtc { get; set; }

    public required string Resource { get; set; }

    public ResourceDataBase ResourceData { get; set; }
}