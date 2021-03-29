

using System.Collections.Generic;
using Core.Enums.Dtos;

namespace Core.UseCases.Lookup.TestApp
{
    public class AddBookInputDto
    {
        public AddBookInputDto()
        {
            BookReview = new HashSet<BookReviewDto>();
        }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Price { get; set; }
        public int AuthorId { get; set; }

        public ICollection<BookReviewDto> BookReview { get; set; }

    }
}
