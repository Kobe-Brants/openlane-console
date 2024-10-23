using BikeConsole.BL.ApiClients.Bike;
using BikeConsole.Core.Interfaces.Repositories;
using BikeConsole.Core.Mapper.DTO_s;
using BikeConsole.Domain;

namespace BikeConsole.BL.Handlers;

public class BikeUpdateHandler : IBikeAuctionMessageHandler
{
    private readonly IGenericRepository<Bike> _bikeRepository;
    private readonly IBikeApiClient _bikeApiClient;

    public BikeUpdateHandler(IGenericRepository<Bike> bikeRepository, IBikeApiClient bikeApiClient)
    {
        _bikeRepository = bikeRepository;
        _bikeApiClient = bikeApiClient;
    }

    public bool CanHandle(string messageType) => messageType == "Bike.Updated";

    public async Task HandleAsync(BikeAuctionMessage message, CancellationToken cancellationToken)
    {
        var bikeContainerId = GetBikeContainerIdFromResourceUrl(message.Resource);
        if (bikeContainerId is null) throw new Exception("BikeContainer not found in resource");

        var bike = _bikeRepository.Find(x => x.Id == message.ResourceData.Id).FirstOrDefault();
        if (bike is null) return;

        var bikes = (await _bikeApiClient.GetBikes(bikeContainerId, cancellationToken))?.Values;
        var bikeToUpdate = bikes?.FirstOrDefault(x => x.Id == message.ResourceData.Id);

        if (bikeToUpdate == null) return;

        bike.Country = message.ResourceData.Country ?? bike.Country;

        _bikeRepository.Update(bike);
        await _bikeRepository.Save(cancellationToken);

        Console.WriteLine($"Bike {bike.Id} updated.");
    }

    private static string? GetBikeContainerIdFromResourceUrl(string resourceUrl)
    {
        return resourceUrl.Split("/").LastOrDefault();
    }
}