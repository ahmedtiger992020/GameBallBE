using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection;
using Core.Entities;

namespace Infrastructure.Context
{
    public partial class GBSampleContext : DbContext
    {
        public GBSampleContext()
        {
        }

        public GBSampleContext(DbContextOptions<GBSampleContext> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }
        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<BookReview> BookReview { get; set; }
        public virtual DbSet<Review> Review { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Author>().HasData(
                  new Author { Nationality = "Egyptian", Name = "Ahmed", Id = 1 },
                  new Author { Nationality = "Poland", Name = "Ali", Id = 2 },
                  new Author { Nationality = "SA", Name = "Omar", Id = 3 }
              );

            modelBuilder.Entity<Book>().HasData(
                  new Book {Id=1, Name = "FirstBook" ,Category="TestCat",AuthorId=1,Price=20}
              );
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); //Fluent APIs
            OnModelCreatingPartial(modelBuilder);

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
