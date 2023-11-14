using Ibge.Domain.Entity;
using Ibge.Infrastructure.Data.Mapping.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ibge.Infrastructure.Data.Mapping;

public class StateMapping : EFMappingEntrypoint<State>, IEFMappingEntrypoint
{
    public override void BuildMapping(EntityTypeBuilder<State> builder)
    {
        builder.Property(c => c.Code).IsRequired();
        builder.Property(c => c.Name).IsRequired();
        builder.Property(c => c.Acronym).HasColumnType("CHARACTER(2)").IsRequired();

        builder.HasIndex(c => c.Code)
            .IsUnique();

        builder.HasIndex(c => c.Acronym)
           .IsUnique();

        builder.HasMany(c => c.Cities)
               .WithOne()
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("States");
    }
}
