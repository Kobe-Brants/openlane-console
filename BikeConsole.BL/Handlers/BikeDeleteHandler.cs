using BikeConsole.BL.ApiClients.Bike;
using BikeConsole.Core.Interfaces.Repositories;
using BikeConsole.Core.Mapper.DTO_s;
using BikeConsole.Domain;

namespace BikeConsole.BL.Handlers;

public class BikeDeletedHandler : IBikeAuctionMessageHandler
{
    private readonly IGenericRepository<Bike> _bikeRepository;

    public BikeDeletedHandler(IGenericRepository<Bike> bikeRepository)
    {
        _bikeRepository = bikeRepository;
    }

    public bool CanHandle(string messageType) => messageType == "Bike.Removed";

    public async Task HandleAsync(BikeAuctionMessage message, CancellationToken cancellationToken)
    {
        var bike = _bikeRepository.Find(x => x.Id == message.ResourceData.Id).FirstOrDefault();
        if (bike is null) return;

        _bikeRepository.Delete(bike);
        await _bikeRepository.Save(cancellationToken);

        Console.WriteLine($"Bike {bike.Id} removed.");
    }
}