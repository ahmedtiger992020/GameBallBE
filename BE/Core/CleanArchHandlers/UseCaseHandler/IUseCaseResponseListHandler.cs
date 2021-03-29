using System.Threading.Tasks;
using Core.Enums.Dtos;

namespace Core
{
    public interface IUseCaseResponseListHandler</*out*/ TUseCaseResponse> 
    {
        Task<bool> HandleUseCase(IOutputPort<ListResultDto<TUseCaseResponse>> _response);
    }
}
