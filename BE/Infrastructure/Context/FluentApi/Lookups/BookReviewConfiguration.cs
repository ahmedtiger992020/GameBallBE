using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities;

namespace Infrastructure.Context
{
    public class BookReviewConfiguration : IEntityTypeConfiguration<BookReview>
    {
        public void Configure(EntityTypeBuilder<BookReview> entityBuilder)
        {
            entityBuilder.HasKey(e => e.Id);

            entityBuilder.Property(e => e.Id).ValueGeneratedOnAdd();

            entityBuilder.HasOne(d => d.Book)
                .WithMany(p => p.BookReviews)
                .HasForeignKey(d => d.BookId);

            entityBuilder.HasOne(d => d.Review)
                .WithMany(p => p.BookReviews)
                .HasForeignKey(d => d.ReviewId);
        }
    }
}
