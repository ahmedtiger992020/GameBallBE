using System.Threading.Tasks;
using Core.Enums.Dtos;

namespace Core
{
    public interface IUseCaseResponseHandler</*out*/ TUseCaseResponse> 
    {
        Task<bool> HandleUseCase(IOutputPort<ResultDto<TUseCaseResponse>> _response);
    }
}
