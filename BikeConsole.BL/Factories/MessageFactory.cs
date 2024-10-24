using System.Text.Json;
using BikeConsole.BL.Converters;
using BikeConsole.Core.Mapper.DTO_s;
using BikeConsole.Core.Mapper.DTO_s.Messages;

namespace BikeConsole.BL.Factories;

public class MessageFactory : IMessageFactory
{
    private readonly JsonSerializerOptions _jsonOptions;

    public MessageFactory()
    {
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new FlexibleDateTimeConverter() }
        };
    }
    
    public BaseMessage? CreateMessage(string json)
    {
        using var doc = JsonDocument.Parse(json);
        var typeProperty = doc.RootElement.GetProperty("type").GetString();

        return typeProperty switch
        {
            "BikeAuction.Created" => JsonSerializer.Deserialize<BikeAuctionCreatedMessage>(json, _jsonOptions),
            "BikeAuction.Extended" => JsonSerializer.Deserialize<BikeAuctionExtendedMessage>(json, _jsonOptions),
            "BikeAuction.Withdrawn" => JsonSerializer.Deserialize<BikeAuctionWithdrawnMessage>(json, _jsonOptions),
            "BikeAuction.Published" => JsonSerializer.Deserialize<BikeAuctionPublishedMessage>(json, _jsonOptions),
            "BikeAuction.Removed" => JsonSerializer.Deserialize<BikeAuctionRemovedMessage>(json, _jsonOptions),
            "Bike.Removed" => JsonSerializer.Deserialize<BikeRemovedMessage>(json, _jsonOptions),
            "Bike.Updated" => JsonSerializer.Deserialize<BikeUpdatedMessage>(json, _jsonOptions),
            _ => throw new ArgumentException($"Unknown message type: {typeProperty}")
        };
    }
}