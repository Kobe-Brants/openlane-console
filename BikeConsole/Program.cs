using BikeConsole.BL;
using BikeConsole.BL.Factories;
using BikeConsole.BL.Services;
using BikeConsole.Core;
using BikeConsole.Core.Mapper;
using BikeConsole.DAL;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BikeConsole;

abstract class Program
{
    static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        var messageFactory = host.Services.GetService<IMessageFactory>();
        var bikeService = host.Services.GetService<IBikeService>();
        if (bikeService is null || messageFactory is null) throw new Exception("Services not found");
        
        while (true)
        {
            Console.WriteLine("Paste the JSON message:");
            var input = Console.ReadLine();

            if (string.IsNullOrEmpty(input)) continue;

            try
            {
                var message = messageFactory.CreateMessage(input);
                if (message is null) throw new Exception("Something went wrong while deserializing message.");
                await bikeService.ProcessMessageAsync(message, CancellationToken.None);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }

    static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton(new AppArguments { Args = args });
                services.AddAutoMapper(typeof(MappingProfile));
                services.RegisterDbContexts(hostContext.Configuration);
                services.RegisterRepositories();
                services.RegisterServices();
                services.RegisterHttpClient();
            });
    }
}

// https://jsonformatter.org/
// dotnet ef migrations add InitialCreate --project BikeConsole.DAL --startup-project BikeConsole
// dotnet ef database update --project BikeConsole.DAL --startup-project BikeConsole