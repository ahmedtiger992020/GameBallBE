using System;
using System.Collections.Generic;


namespace Core.Entities
{
    public  class Review : BaseEntity
    {
        public Review()
        {
            BookReviews = new HashSet<BookReview>();
        }

        public string Text { get; set; }
        public short Rating { get; set; }

        public virtual ICollection<BookReview> BookReviews { get; set; }
    }
}
