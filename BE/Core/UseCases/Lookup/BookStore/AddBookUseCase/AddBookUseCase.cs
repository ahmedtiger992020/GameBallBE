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
    public class AddBookUseCase : BaseUseCase, IAddBookUseCase
    {

        #region Props  

        public IBookRepository _bookRepository { get; }
        public IAuthorRepository _authorRepository { get; }
        public IBookRepository BookRepository { get; set; }

        #endregion

        #region Ctor
        public AddBookUseCase(IUnitOfWork UnitOfWork, IBookRepository BookRepository, IAuthorRepository AuthorRepository, IReviewRepository ReviewRepository, IConfiguration Configuration, ILogger Logger, IMapper Mapper)
            : base(UnitOfWork, Configuration, Logger, Mapper)
        {
            _bookRepository = BookRepository;
            _authorRepository = AuthorRepository;
        }

        #endregion
        public async Task<bool> HandleUseCase(AddBookInputDto _request, IOutputPort<ResultDto<bool>> Presenter)
        {
            //Validate of Incoming Request
            await new BookSharedMethods(_authorRepository).ValidateToSave(_request);


            #region Insert New Book
            Book book = _mapper.Map<Book>(_request);

            await _bookRepository.InsertAsync(book);
            if (_request.BookReview?.Any() ?? default)
            {
                book.BookReviews =_request.BookReview.Select(a => new BookReview()
                {
                    Review = new Review { Rating = a.Rating, Text = a.Text }
                }).ToList();
            }
            await _unitOfWork.Commit();

            #endregion

            Presenter.HandlePresenter(new ResultDto<bool>(true));
            return true;
        }
    }
}
