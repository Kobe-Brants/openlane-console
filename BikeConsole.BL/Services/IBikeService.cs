using BikeConsole.Core.Mapper.DTO_s;

namespace BikeConsole.BL.Services;

public interface IBikeService
{
    Task ProcessMessageAsync(BaseMessage message, CancellationToken cancellationToken);
}