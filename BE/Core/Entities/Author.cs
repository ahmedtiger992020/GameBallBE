using System;
using System.Collections.Generic;
using Core.Entities;


namespace Core.Entities
{
    public partial class Author : BaseEntity
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }

        public string Name { get; set; }
        public string Nationality { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
