using System;
using System.Collections.Generic;


namespace Core.Entities
{
    public class BookReview : BaseEntity
    {
        public int? BookId { get; set; }
        public int? ReviewId { get; set; }

        public virtual Book Book { get; set; }
        public virtual Review Review { get; set; }
    }
}
