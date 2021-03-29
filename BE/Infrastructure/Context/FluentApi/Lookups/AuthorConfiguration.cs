using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities;

namespace Infrastructure.Context
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> entityBuilder)
        {
            entityBuilder.HasKey(e => e.Id);

            entityBuilder.Property(e => e.Id).ValueGeneratedOnAdd();

            entityBuilder.Property(e => e.Name).HasMaxLength(100);

            entityBuilder.Property(e => e.Nationality).HasMaxLength(100);
        }
    }
}
