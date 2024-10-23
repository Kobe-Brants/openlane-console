using BikeConsole.DAL.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace BikeConsole.DAL.DbContexts
{
    public class BikeContext : DbContext
    {
        public BikeContext(DbContextOptions<BikeContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BikeConfiguration).Assembly, x => x.Namespace != null && x.Namespace.StartsWith("BikeConsole.DAL.EntityConfigurations"));
        }
    }
}
