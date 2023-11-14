using Ibge.Domain.Entity;
using Ibge.Infrastructure.Data.Mapping.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ibge.Infrastructure.Data.Mapping;

public class UserMapping : EFMappingEntrypoint<User>, IEFMappingEntrypoint
{
    public override void BuildMapping(EntityTypeBuilder<User> builder)
    {
        builder.Property(c => c.Email).IsRequired();
        builder.Property(c => c.Name).IsRequired();
        builder.Property(c => c.Password).IsRequired();
        builder.Property(c => c.IsAdmin).IsRequired();

        builder.ToTable("Users");
    }
}
