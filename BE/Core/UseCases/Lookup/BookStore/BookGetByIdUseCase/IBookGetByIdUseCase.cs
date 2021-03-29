
using Core;

using Core.Enums.Dtos;

namespace Core.UseCases.Lookup.TestApp
{
    public interface IBookGetByIdUseCase: IUseCaseRequestResponseHandler<int , BookGetByIdOutputDto>
    {
    }
}
