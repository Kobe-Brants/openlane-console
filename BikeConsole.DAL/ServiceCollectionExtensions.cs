using BikeConsole.Core.Interfaces.Repositories;
using BikeConsole.DAL.DbContexts;
using BikeConsole.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BikeConsole.DAL;

public static class ServiceCollectionExtensions
{
    public static void RegisterDbContexts(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<BikeContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("BikeConsole.DAL")));
    }   

    public static void RegisterRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IGenericRepository<Bike>, GenericRepository<Bike, BikeContext>>();
        serviceCollection.AddScoped<IGenericRepository<BikeContainer>, GenericRepository<BikeContainer, BikeContext>>();
        serviceCollection.AddScoped<IGenericRepository<Document>, GenericRepository<Document, BikeContext>>();
        serviceCollection.AddScoped<IGenericRepository<Tax>, GenericRepository<Tax, BikeContext>>();
    }
}