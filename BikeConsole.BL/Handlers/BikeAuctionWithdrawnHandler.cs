using BikeConsole.Core.Interfaces.Repositories;
using BikeConsole.Core.Mapper.DTO_s.Messages;
using BikeConsole.Domain;
using Microsoft.EntityFrameworkCore;

namespace BikeConsole.BL.Handlers;

public class BikeAuctionWithdrawnHandler : BaseAuctionMessageHandler<BikeAuctionWithdrawnMessage>
{
    private readonly IGenericRepository<BikeContainer> _bikeContainerRepository;

    public BikeAuctionWithdrawnHandler(IGenericRepository<BikeContainer> bikeContainerRepository)
    {
        _bikeContainerRepository = bikeContainerRepository;
    }

    public override bool CanHandle(string messageType) => messageType == "BikeAuction.Withdrawn";

    public override async Task HandleTypedAsync(BikeAuctionWithdrawnMessage message, CancellationToken cancellationToken)
    {
        var bikeContainerId = GetBikeContainerIdFromResourceUrl(message.Resource);
        if (bikeContainerId is null) throw new Exception("BikeContainer not found in resource");

        var bikeContainer = _bikeContainerRepository.Find(x => x.ContainerId == bikeContainerId).Include(x => x.Bikes)
            .FirstOrDefault();
        if (bikeContainer is null) return;

        var bikeToRemove = bikeContainer.Bikes.FirstOrDefault(x => x.Id == message.ResourceData.Id);
        if (bikeToRemove is null) return;

        bikeContainer.Bikes.Remove(bikeToRemove);

        _bikeContainerRepository.Update(bikeContainer);
        await _bikeContainerRepository.Save(cancellationToken);
        Console.WriteLine($"Bike {bikeToRemove.Id} removed from auction.");
    }


    private static string? GetBikeContainerIdFromResourceUrl(string resourceUrl)
    {
        return resourceUrl.Split("/").LastOrDefault();
    }
}