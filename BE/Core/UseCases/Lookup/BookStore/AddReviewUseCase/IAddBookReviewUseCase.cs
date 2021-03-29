
using Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Enums.Dtos;

namespace Core.UseCases.Lookup.TestApp
{
    public interface IAddBookReviewUseCase : IUseCaseRequestResponseHandler<AddBookReviewInputDto, bool>
    {
       
    }
}