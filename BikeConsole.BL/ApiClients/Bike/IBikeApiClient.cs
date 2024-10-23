using BikeConsole.Core.Mapper.DTO_s.Bike.Response;

namespace BikeConsole.BL.ApiClients.Bike;

public interface IBikeApiClient
{
    Task<BikeResponse?> GetBikes(string? bikeContainerId, CancellationToken cancellationToken);
}