using AutoMapper;
using System.Linq;
using Core.Entities;
using Core.Enums.Dtos;
using Core.UseCases.Lookup.TestApp;

namespace Infrastructure.Mapping
{
    public class BookMapping : Profile
    {
        public BookMapping()
        {
            CreateMap<Book, AddBookInputDto>()
            .ReverseMap();

            CreateMap<Book, BookGetByIdOutputDto>()
                .ForMember(dest => dest.BookReview, opt => opt.MapFrom(src => src.BookReviews.Count > default(int) ? src.BookReviews.Select(a => new BookReviewDto { Rating = a.Review.Rating, Text = a.Review.Text }) : null))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.Name))
                .ForMember(dest => dest.AvgRating, opt => opt.MapFrom(src => src.BookReviews.Count > default(int) ? src.BookReviews.Average(a => a.Review.Rating) : 0))
            .ReverseMap();


            CreateMap<Book, BookGetAllOutputDto>().ReverseMap();
            CreateMap<Author, DDLDto>().ReverseMap();
            CreateMap<Book, DDLDto>().ReverseMap();

        }

    }

}
