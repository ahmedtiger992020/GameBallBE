
using AutoMapper;
using Core;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Enums.Dtos;
using Core.Intefaces;

namespace Core.UseCases.Lookup.TestApp
{
    public class BookGetDDLUseCase : BaseUseCase, IBookGetDDLUseCase
    {
        #region Props
        public IBookRepository _bookRepository { get; set; }
        #endregion

        #region Ctor
        public BookGetDDLUseCase(IUnitOfWork UnitOfWork, IBookRepository BookRepository, IConfiguration Configuration, ILogger Logger, IMapper Mapper)
            : base(UnitOfWork, Configuration, Logger, Mapper)
        {
            _bookRepository = BookRepository;
        }
        #endregion

        public async Task<bool> HandleUseCase(IOutputPort<ListResultDto<DDLDto>> outputPort)
        {

            IEnumerable<Entities.Book> DataList = null;

            DataList = await _bookRepository.GetWhereAsync();


            outputPort.HandlePresenter(new ListResultDto<DDLDto>(_mapper.Map<List<DDLDto>>(DataList), DataList.Count()));
            return true;
        }


    }
}
