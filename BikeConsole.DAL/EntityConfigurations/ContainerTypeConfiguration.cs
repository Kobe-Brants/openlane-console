using BikeConsole.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BikeConsole.DAL.EntityConfigurations;

public class ContainerConfiguration : IEntityTypeConfiguration<BikeContainer>
{
    public void Configure(EntityTypeBuilder<BikeContainer> builder)
    {
        builder.ToTable(nameof(BikeContainer));
        
        builder.HasMany(x => x.Bikes)
            .WithOne(x => x.BikeContainer)
            .HasPrincipalKey(x => x.ContainerId)
            .HasForeignKey(x => x.BikeContainerId);

        builder.HasKey(x => x.ContainerId);
    }
}