
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
    public class AuthorGetDDLUseCase : BaseUseCase, IAuthorGetDDLUseCase
    {
        #region Props
        public IAuthorRepository _authorRepository { get; set; }
        #endregion

        #region Ctor
        public AuthorGetDDLUseCase(IUnitOfWork UnitOfWork, IAuthorRepository AuthorRepository, IConfiguration Configuration, ILogger Logger, IMapper Mapper)
            : base(UnitOfWork, Configuration, Logger, Mapper)
        {
            _authorRepository = AuthorRepository;
        }
        #endregion

        public async Task<bool> HandleUseCase(IOutputPort<ListResultDto<DDLDto>> outputPort)
        {

            IEnumerable<Entities.Author> DataList = null;

            DataList = await _authorRepository.GetWhereAsync();


            outputPort.HandlePresenter(new ListResultDto<DDLDto>(_mapper.Map<List<DDLDto>>(DataList), DataList.Count()));
            return true;
        }


    }
}
