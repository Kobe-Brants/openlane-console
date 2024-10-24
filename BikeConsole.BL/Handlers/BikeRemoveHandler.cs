using BikeConsole.Core.Interfaces.Repositories;
using BikeConsole.Core.Mapper.DTO_s.Messages;
using BikeConsole.Domain;

namespace BikeConsole.BL.Handlers;

public class BikeRemoveHandler : BaseAuctionMessageHandler<BikeRemovedMessage>
{
    private readonly IGenericRepository<Bike> _bikeRepository;

    public BikeRemoveHandler(IGenericRepository<Bike> bikeRepository)
    {
        _bikeRepository = bikeRepository;
    }

    public override bool CanHandle(string messageType) => messageType == "Bike.Removed";

    public override async Task HandleTypedAsync(BikeRemovedMessage message, CancellationToken cancellationToken)
    {
        var bike = _bikeRepository.Find(x => x.Id == message.ResourceData.Id).FirstOrDefault();
        if (bike is null) return;

        _bikeRepository.Delete(bike);
        await _bikeRepository.Save(cancellationToken);

        Console.WriteLine($"Bike {bike.Id} removed.");
    }
}