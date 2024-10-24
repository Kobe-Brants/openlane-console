using BikeConsole.BL.ApiClients.Bike;
using BikeConsole.Core.Interfaces.Repositories;
using BikeConsole.Core.Mapper.DTO_s.Messages;
using BikeConsole.Domain;

namespace BikeConsole.BL.Handlers;

public class BikeUpdateHandler : BaseAuctionMessageHandler<BikeUpdatedMessage>
{
    private readonly IGenericRepository<Bike> _bikeRepository;
    private readonly IBikeApiClient _bikeApiClient;

    public BikeUpdateHandler(IGenericRepository<Bike> bikeRepository, IBikeApiClient bikeApiClient)
    {
        _bikeRepository = bikeRepository;
        _bikeApiClient = bikeApiClient;
    }

    public override bool CanHandle(string messageType) => messageType == "Bike.Updated";

    public override async Task HandleTypedAsync(BikeUpdatedMessage message, CancellationToken cancellationToken)
    {
        var bikeContainerId = GetBikeContainerIdFromResourceUrl(message.Resource);
        if (bikeContainerId is null) throw new Exception("BikeContainer not found in resource");

        var bike = _bikeRepository.Find(x => x.Id == message.ResourceData.Id).FirstOrDefault();
        if (bike is null) return;

        var bikes = (await _bikeApiClient.GetBikes(bikeContainerId, cancellationToken))?.Values;
        var bikeToUpdate = bikes?.FirstOrDefault(x => x.Id == message.ResourceData.Id);

        if (bikeToUpdate == null) return;

        _bikeRepository.Update(bike);
        await _bikeRepository.Save(cancellationToken);

        Console.WriteLine($"Bike {bike.Id} updated.");
    }


    private static string? GetBikeContainerIdFromResourceUrl(string resourceUrl)
    {
        return resourceUrl.Split("/").LastOrDefault();
    }
}