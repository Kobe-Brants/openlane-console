using System.Text.Json;
using BikeConsole.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BikeConsole.DAL.EntityConfigurations;

public class BikeConfiguration : IEntityTypeConfiguration<Bike>
{
    public void Configure(EntityTypeBuilder<Bike> builder)
    {
        builder.ToTable(nameof(Bike));

        builder.HasMany(x => x.Documents)
            .WithOne(x => x.Bike)
            .HasPrincipalKey(x => x.Id)
            .HasForeignKey(x => x.DocumentId);
        
        builder.HasMany(x => x.Taxes)
            .WithOne(x => x.Bike)
            .HasPrincipalKey(x => x.Id)
            .HasForeignKey(x => x.TaxId);

        builder.Property(x => x.Equipments)
            .HasConversion(new ValueConverter<List<string>, string>(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>()));

        builder.HasKey(x => x.Id);
    }
}