using GB.Core;
using System;
using System.Threading.Tasks;
using Core.Entities;
using Core.Enums.Dtos;
using Core.Intefaces;

namespace Core.UseCases.Lookup.TestApp
{
    public class BookSharedMethods
    {

        #region Vars
        public IAuthorRepository _authorRepository { get; set; }
        public IBookRepository _bookRepository { get; set; }

        #endregion
        #region Ctor
        
        public BookSharedMethods(IAuthorRepository AuthorRepository)
        {
            _authorRepository = AuthorRepository;
        }
        public BookSharedMethods(IBookRepository BookRepository)
        {
            _bookRepository = BookRepository;
        }
        #endregion

        internal async Task<bool> ValidateToSave(AddBookInputDto _request)
        {
            #region User Input Validation
            if (_request == null)
                throw new ValidationsException("InvalidRequest");
            else if (string.IsNullOrWhiteSpace(_request.Name))
                throw new ValidationsException("Name Is Required");
            else if (string.IsNullOrWhiteSpace(_request.Category))
                throw new ValidationsException("Category Is Required");
            else if (string.IsNullOrWhiteSpace(_request.Price))
                throw new ValidationsException("Price Is Required");
            else if (_request.AuthorId<=default(int))
                throw new ValidationsException("Author Is Required");
            #endregion

            #region Db Validation
            else if (!await GetAuthorIfFound(_request.AuthorId))
                throw new ValidationsException("Author Not Found in Our DataBase");
            #endregion

            return true;
        }

        internal async Task<Book> GetBookItemIfValid(int _request)
        {
            #region User Input Validation
            if (_request <= default(int))
                throw new ValidationsException("Invalid Book Id");

            #endregion

            #region DB Validation
            string includeProperties = $"BookReviews.Review,Author";

            Book bookObj = await _bookRepository.GetFirstOrDefaultAsync(a=>a.Id==_request, includeProperties);
            if (bookObj == null)
                throw new EntityNotFoundException(nameof(Book), _request);

            #endregion

            return bookObj;
        }

        internal async Task<bool> GetAuthorIfFound(int authorId)
        {
          
            #region DB Validation

            bool authorObj = await _authorRepository.GetAnyAsync(a=>a.Id==authorId);
            if (authorObj)
                return true;

            #endregion
            
            return false;
        }
    }
}
