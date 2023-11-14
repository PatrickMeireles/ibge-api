using Ibge.Domain.Entity;
using Ibge.Infrastructure.Data.Mapping.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ibge.Infrastructure.Data.Mapping;

public class CityMapping : EFMappingEntrypoint<City>, IEFMappingEntrypoint
{
    public override void BuildMapping(EntityTypeBuilder<City> builder)
    {
        builder.Property(c => c.Code).IsRequired();
        builder.Property(c => c.Name).IsRequired();

        builder.HasIndex(c => c.Code)
            .IsUnique();

        builder
            .HasOne(c => c.State)
            .WithMany()
            .HasForeignKey(c => c.StateId)
            .HasPrincipalKey(c => c.Id);

        builder.ToTable("Cities");

    }
}
