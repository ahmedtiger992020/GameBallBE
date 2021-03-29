using AutoMapper;
using Microsoft.Extensions.Configuration;
using Serilog;
using Core.Intefaces;

namespace Core.UseCases
{
    public class BaseUseCase
    {
        public IUnitOfWork _unitOfWork { get; set; }
        public IConfiguration _configuration { get; set; }
        public readonly ILogger _logger;
        public  IMapper _mapper;

        public BaseUseCase(IUnitOfWork UnitOfWork, IConfiguration Configuration, ILogger Logger, IMapper Mapper)
        {
            _unitOfWork = UnitOfWork;
            _configuration = Configuration;
            _logger = Logger;
            _mapper = Mapper;
        }
    }
}