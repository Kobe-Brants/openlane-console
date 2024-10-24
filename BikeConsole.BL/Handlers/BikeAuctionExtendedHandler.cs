using BikeConsole.BL.ApiClients.Bike;
using BikeConsole.Core.Interfaces.Repositories;
using BikeConsole.Core.Mapper.DTO_s.Messages;
using BikeConsole.Domain;
using Microsoft.EntityFrameworkCore;

namespace BikeConsole.BL.Handlers;

public class BikeAuctionExtendedHandler : BaseAuctionMessageHandler<BikeAuctionExtendedMessage>
{
    private readonly IGenericRepository<BikeContainer> _bikeContainerRepository;
    private readonly IBikeApiClient _bikeApiClient;

    public BikeAuctionExtendedHandler(IGenericRepository<BikeContainer> bikeContainerRepository,
        IBikeApiClient bikeApiClient)
    {
        _bikeContainerRepository = bikeContainerRepository;
        _bikeApiClient = bikeApiClient;
    }

    public override bool CanHandle(string messageType) => messageType == "BikeAuction.Extended";

    public override async Task HandleTypedAsync(BikeAuctionExtendedMessage message, CancellationToken cancellationToken)
    {
        var bikeContainerId = GetBikeContainerIdFromResourceUrl(message.Resource);
        if (bikeContainerId is null) throw new Exception("BikeContainer not found in resource");

        var bikes = (await _bikeApiClient.GetBikes(bikeContainerId, cancellationToken))?.Values;
        if (bikes is null) return;

        var bikeContainer = _bikeContainerRepository.Find(x => x.ContainerId == bikeContainerId).Include(x => x.Bikes)
            .FirstOrDefault();
        if (bikeContainer is null) throw new Exception("Bike container doesn't exist yet.");

        var newBike = bikes.FirstOrDefault(x => x.Id == message.ResourceData.Id);
        if (newBike is null) return;

        bikeContainer.Bikes.Add(newBike);
        _bikeContainerRepository.Update(bikeContainer);
        await _bikeContainerRepository.Save(cancellationToken);
        Console.WriteLine($"Bike {newBike.Id} added to auction {bikeContainer.ContainerId}.");
    }
    
    private static string? GetBikeContainerIdFromResourceUrl(string resourceUrl)
    {
        return resourceUrl.Split("/").LastOrDefault();
    }
}