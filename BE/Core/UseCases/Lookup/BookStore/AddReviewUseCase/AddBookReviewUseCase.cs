using AutoMapper;
using Cloudikka.PolylineAlgorithm;
using Core;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Core.Entities;
using Core.Enums.Dtos;
using Core.Intefaces;

namespace Core.UseCases.Lookup.TestApp
{
    public class AddBookReviewUseCase : BaseUseCase, IAddBookReviewUseCase
    {

        #region Props  

        public IReviewRepository _reviewRepository { get; set; }
        public IBookRepository _bookRepository { get; set; }

        #endregion

        #region Ctor
        public AddBookReviewUseCase(IUnitOfWork UnitOfWork, IReviewRepository ReviewRepository,IBookRepository BookRepository, IConfiguration Configuration, ILogger Logger, IMapper Mapper)
            : base(UnitOfWork, Configuration, Logger, Mapper)
        {
            _reviewRepository = ReviewRepository;
            _bookRepository = BookRepository;
        }

        #endregion
        public async Task<bool> HandleUseCase(AddBookReviewInputDto _request, IOutputPort<ResultDto<bool>> Presenter)
        {
            //Validate of Incoming Request
            Book book = await new BookSharedMethods(_bookRepository).GetBookItemIfValid(_request.BookId);
            if(book!=null)
            {
                book.BookReviews = _request.BookReview.Select(a => new BookReview()
                {
                    Review = new Review { Rating = a.Rating, Text = a.Text }
                }).ToList();
            }
            #region Insert New Book
             _bookRepository.Update(book);
           
            await _unitOfWork.Commit();

            #endregion

            Presenter.HandlePresenter(new ResultDto<bool>(true));
            return true;
        }
    }
}
