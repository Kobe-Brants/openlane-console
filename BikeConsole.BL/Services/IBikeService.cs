using BikeConsole.Core.Mapper.DTO_s;

namespace BikeConsole.BL.Services;

public interface IBikeService
{
    Task ProcessMessageAsync(BikeAuctionMessage message, CancellationToken cancellationToken);
    // Task<HandlerResult<ProductResponse>> GetProductAsync(string productId, CancellationToken cancellationToken);
    // Task<HandlerResult<PagedResult<ProductResponse>>> GetProductsAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    // Task<HandlerResult<ProductResponse>> CreateProductAsync(Product product, CancellationToken cancellationToken);
    // Task<HandlerResult<ProductResponse>> UpdateProductAsync(string productId, Product product, CancellationToken cancellationToken);
    // Task<HandlerResult> DeleteProductAsync(string productId, CancellationToken cancellationToken);
}