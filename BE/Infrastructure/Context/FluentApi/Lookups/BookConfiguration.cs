using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities;

namespace Infrastructure.Context
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> entityBuilder)
        {
            entityBuilder.HasKey(e => e.Id);

            entityBuilder.Property(e => e.Id).ValueGeneratedOnAdd();

            entityBuilder.Property(e => e.Category).HasMaxLength(100);

            entityBuilder.Property(e => e.Name).HasMaxLength(100);

            entityBuilder.Property(e => e.Price).HasColumnType("decimal(18, 0)");

            entityBuilder.HasOne(d => d.Author)
                .WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId);
        }
    }
}
