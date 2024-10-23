using BikeConsole.BL.ApiClients.Bike;
using BikeConsole.BL.Handlers;
using BikeConsole.BL.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using Polly;

namespace BikeConsole.BL;

public static class ServiceCollectionExtensions
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IBikeService, BikeService>();

        services.AddScoped<IBikeAuctionMessageHandler, BikeAuctionCreatedHandler>();
        services.AddScoped<IBikeAuctionMessageHandler, BikeAuctionDeletedHandler>();
        services.AddScoped<IBikeAuctionMessageHandler, BikeAuctionExtendedHandler>();
        services.AddScoped<IBikeAuctionMessageHandler, BikeAuctionWithdrawnHandler>();
        services.AddScoped<IBikeAuctionMessageHandler, BikeAuctionPublishedHandler>();
        
        services.AddScoped<IBikeAuctionMessageHandler, BikeDeletedHandler>();
        services.AddScoped<IBikeAuctionMessageHandler, BikeUpdateHandler>();
    }

    public static void RegisterHttpClient(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHttpClient<IBikeApiClient, BikeApiClient>("bike").AddResilienceHandler(
            "bike-pipeline", b =>
            {
                b.AddRetry(new HttpRetryStrategyOptions()
                {
                    MaxRetryAttempts = 3,
                    Delay = TimeSpan.FromSeconds(2),
                    BackoffType = DelayBackoffType.Exponential
                });
                b.AddTimeout(TimeSpan.FromSeconds(5));
            });
    }
}