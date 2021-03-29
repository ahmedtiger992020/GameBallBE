using System.Collections.Generic;

namespace Core.UseCases.Lookup.TestApp
{
    public class BookGetByIdOutputDto
    {
        public BookGetByIdOutputDto()
        {
            BookReview = new List<BookReviewDto>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Price { get; set; }
        public string Author { get; set; }
        public string AvgRating { get; set; }
        public List<BookReviewDto> BookReview { get; set; }

    }
}
