using System;
using System.Collections.Generic;


namespace Core.Entities
{
    public class Book : BaseEntity
    {
        public Book()
        {
            BookReviews = new HashSet<BookReview>();
        }

        public string Name { get; set; }
        public string Category { get; set; }
        public decimal? Price { get; set; }
        public int? AuthorId { get; set; }

        public virtual Author Author { get; set; }
        public virtual ICollection<BookReview> BookReviews { get; set; }
    }
}
