using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities;

namespace Infrastructure.Context
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> entityBuilder)
        {
            entityBuilder.HasKey(e => e.Id);

            entityBuilder.Property(e => e.Id).ValueGeneratedOnAdd();

            entityBuilder.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(250);

            entityBuilder.Property(e => e.Rating)
                .IsRequired();
        }
    }
}
