using BikeConsole.BL.ApiClients.Bike;
using BikeConsole.Core.Interfaces.Repositories;
using BikeConsole.Core.Mapper.DTO_s;
using BikeConsole.Domain;
using Microsoft.EntityFrameworkCore;

namespace BikeConsole.BL.Handlers;

public class BikeAuctionDeletedHandler : IBikeAuctionMessageHandler
{
    private readonly IGenericRepository<BikeContainer> _bikeContainerRepository;

    public BikeAuctionDeletedHandler(IGenericRepository<BikeContainer> bikeContainerRepository)
    {
        _bikeContainerRepository = bikeContainerRepository;
    }

    public bool CanHandle(string messageType) => messageType == "BikeAuction.Deleted";

    public async Task HandleAsync(BikeAuctionMessage message, CancellationToken cancellationToken)
    {
        var bikeContainerId = GetBikeContainerIdFromResourceUrl(message.Resource);
        if (bikeContainerId is null) throw new Exception("BikeContainer not found in resource");

        var bikeContainer = _bikeContainerRepository.Find(x => x.ContainerId == bikeContainerId).Include(x => x.Bikes).FirstOrDefault();
        if (bikeContainer is null) return;

        _bikeContainerRepository.Delete(bikeContainer);
        await _bikeContainerRepository.Save(cancellationToken);

        Console.WriteLine($"Bike container {bikeContainerId} removed.");
    }

    private static string? GetBikeContainerIdFromResourceUrl(string resourceUrl)
    {
        return resourceUrl.Split("/").LastOrDefault();
    }
}