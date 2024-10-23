using System.Text.Json;
using AutoMapper;
using BikeConsole.Core.Mapper.DTO_s.Bike.Response;
using Microsoft.Extensions.Configuration;

namespace BikeConsole.BL.ApiClients.Bike;

public class BikeApiClient : IBikeApiClient
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _client;

    public BikeApiClient(IConfiguration configuration, HttpClient client)
    {
        _configuration = configuration;
        _client = client;
    }

    public async Task<BikeResponse?> GetBikes(string? bikeContainerId, CancellationToken cancellationToken)
    {
        var baseUrl = _configuration.GetValue<string>("ApiClients:BikesBaseUrl");
        var response = await _client.GetAsync($"{baseUrl}/Bikes?BikeContainerId={bikeContainerId}", cancellationToken);
        response.EnsureSuccessStatusCode();
        
        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<BikeResponse>(json);
    }
}