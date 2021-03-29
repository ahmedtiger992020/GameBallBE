

using System.Collections.Generic;
using Core.Enums.Dtos;

namespace Core.UseCases.Lookup.TestApp
{
    public class AddBookReviewInputDto
    {
        public int BookId { get; set; }
        public List<BookReviewDto> BookReview { get; set; }
    }
}
