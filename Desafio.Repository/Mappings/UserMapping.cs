using Desafio.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio.Repository.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(x => x.Phones)
                .WithOne();

            builder.OwnsOne(x => x.Password, p => p.Property(f => f.Valor));

            builder.OwnsOne(x => x.Email, p => p.Property(f => f.Valor));

        }

    }
}