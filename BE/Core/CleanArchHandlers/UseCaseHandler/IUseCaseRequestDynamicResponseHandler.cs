
using System.Threading.Tasks;
using Core.Enums.Dtos;

namespace Core

{
    public interface IUseCaseRequestDynamicResponseHandler<in TUseCaseRequest, dynamic>
    {
        Task<bool> HandleUseCase(TUseCaseRequest _request, IOutputPort<ResultDto<dynamic>> _response);
    }
}
