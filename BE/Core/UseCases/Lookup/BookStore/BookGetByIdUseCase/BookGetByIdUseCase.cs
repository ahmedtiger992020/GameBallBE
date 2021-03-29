
using AutoMapper;
using Core;
using Serilog;
using System.Threading.Tasks;
using Core.Enums.Dtos;
using Core.Intefaces;
using Microsoft.Extensions.Configuration;


namespace Core.UseCases.Lookup.TestApp
{
    public class BookGetByIdUseCase : BaseUseCase, IBookGetByIdUseCase
    {
        #region Props  

        public IBookRepository _bookRepository { get; }

        #endregion

        #region Ctor
        public BookGetByIdUseCase(IUnitOfWork UnitOfWork, IBookRepository BookRepository, IAuthorRepository AuthorRepository, IReviewRepository ReviewRepository, IConfiguration Configuration, ILogger Logger, IMapper Mapper)
            : base(UnitOfWork, Configuration, Logger, Mapper)
        {
            _bookRepository = BookRepository;
        }
        #endregion

        public async Task<bool> HandleUseCase(int _request, IOutputPort<ResultDto<BookGetByIdOutputDto>> _presenter)
        {
            Entities.Book _book = await new BookSharedMethods(_bookRepository).GetBookItemIfValid(_request);

            _presenter.HandlePresenter(new ResultDto<BookGetByIdOutputDto>(_mapper.Map<BookGetByIdOutputDto>(_book)));
            return true;
        }
    }
}
