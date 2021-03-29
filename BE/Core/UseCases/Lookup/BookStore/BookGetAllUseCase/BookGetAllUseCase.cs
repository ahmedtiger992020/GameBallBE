using AutoMapper;
using Core;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;
using Core.Enums.Dtos;
using Core.Intefaces;

namespace Core.UseCases.Lookup.TestApp
{
    public class BookGetAllUseCase : BaseUseCase, IBookGetAllUseCase
    {
        #region Props
        public IBookRepository _bookRepository { get; set; }
        #endregion

        #region Ctor
        public BookGetAllUseCase(IUnitOfWork UnitOfWork, IBookRepository BookRepository, IConfiguration Configuration, ILogger Logger, IMapper Mapper)
            : base(UnitOfWork, Configuration, Logger, Mapper)
        {
            _bookRepository = BookRepository;
        }
        #endregion
        public async Task<bool> HandleUseCase(BookGetAllInputDto _request, IOutputPort<ListResultDto<BookGetAllOutputDto>> _presenter)
        {

            #region Filter
            Expression<Func<Book, bool>> filter = x => (string.IsNullOrEmpty(_request.Name) || x.Name.Contains(_request.Name.Trim()));

            #endregion

            #region Sorting
            Expression<Func<Book, string>> sorting = null;
            switch (_request.SortingModel.SortingExpression)
            {
                case "BookId": sorting = s => s.Id.ToString(); break;
                case "BookName": sorting = s => s.Name; break;
                default: sorting =s => s.Id.ToString(); break;
            }
            #endregion
            List<Book> DataList = await _bookRepository.GetPageAsync(_request.Paging.PageNumber
                    , _request.Paging.PageSize, filter, sorting, _request.SortingModel.SortingDirection);
            _presenter.HandlePresenter(new ListResultDto<BookGetAllOutputDto>(_mapper.Map<List<BookGetAllOutputDto>>(DataList)
                , DataList.Count()));
            return true;


        }




    }
}
