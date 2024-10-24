using BikeConsole.BL.ApiClients.Bike;
using BikeConsole.BL.Factories;
using BikeConsole.BL.Handlers;
using BikeConsole.BL.Services;
using BikeConsole.Core.Mapper.DTO_s.Messages;
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
        services.AddScoped<IBikeAuctionMessageHandler<BikeAuctionCreatedMessage>, BikeAuctionCreatedHandler>();

        services.AddScoped<IBikeAuctionMessageHandler, BikeAuctionRemovedHandler>();
        services.AddScoped<IBikeAuctionMessageHandler<BikeAuctionRemovedMessage>, BikeAuctionRemovedHandler>();

        
        services.AddScoped<IBikeAuctionMessageHandler, BikeAuctionExtendedHandler>();
        services.AddScoped<IBikeAuctionMessageHandler<BikeAuctionExtendedMessage>, BikeAuctionExtendedHandler>();

        
        services.AddScoped<IBikeAuctionMessageHandler, BikeAuctionWithdrawnHandler>();
        services.AddScoped<IBikeAuctionMessageHandler<BikeAuctionWithdrawnMessage>, BikeAuctionWithdrawnHandler>();

        services.AddScoped<IBikeAuctionMessageHandler, BikeAuctionPublishedHandler>();
        services.AddScoped<IBikeAuctionMessageHandler<BikeAuctionPublishedMessage>, BikeAuctionPublishedHandler>();

        services.AddScoped<IBikeAuctionMessageHandler, BikeRemoveHandler>();
        services.AddScoped<IBikeAuctionMessageHandler<BikeRemovedMessage>, BikeRemoveHandler>();
        
        services.AddScoped<IBikeAuctionMessageHandler, BikeUpdateHandler>();
        services.AddScoped<IBikeAuctionMessageHandler<BikeUpdatedMessage>, BikeUpdateHandler>();
        
        services.AddScoped<IMessageFactory, MessageFactory>();
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