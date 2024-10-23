using BikeConsole.BL.ApiClients.Bike;
using BikeConsole.Core.Interfaces.Repositories;
using BikeConsole.Core.Mapper.DTO_s;
using BikeConsole.Domain;

namespace BikeConsole.BL.Handlers;

public class BikeAuctionCreatedHandler : IBikeAuctionMessageHandler
{
    private readonly IGenericRepository<BikeContainer> _bikeContainerRepository;
    private readonly IBikeApiClient _bikeApiClient;

    public BikeAuctionCreatedHandler(IGenericRepository<BikeContainer> bikeContainerRepository, IBikeApiClient bikeApiClient)
    {
        _bikeContainerRepository = bikeContainerRepository;
        _bikeApiClient = bikeApiClient;
    }

    public bool CanHandle(string messageType) => messageType == "BikeAuction.Created";

    public async Task HandleAsync(BikeAuctionMessage message, CancellationToken cancellationToken)
    {
        var bikeContainerId = GetBikeContainerIdFromResourceUrl(message.Resource);
        if (bikeContainerId is null) throw new Exception("BikeContainer not found in resource");

        var bikes = (await _bikeApiClient.GetBikes(bikeContainerId, cancellationToken))?.Values;
        var bikeToAdd = bikes?.FirstOrDefault(x => x.Id == message.ResourceData.Id);
        if (bikeToAdd is null) return;

        var newContainer = new BikeContainer
        {
            ContainerId = bikeContainerId,
            Bikes = new List<Bike> { bikeToAdd }
        };

        await _bikeContainerRepository.Add(newContainer, cancellationToken);
        await _bikeContainerRepository.Save(cancellationToken);

        Console.WriteLine($"Bike container {bikeContainerId} published with bike {bikeToAdd.Id}.");
    }

    private static string? GetBikeContainerIdFromResourceUrl(string resourceUrl)
    {
        return resourceUrl.Split("/").LastOrDefault();
    }
}