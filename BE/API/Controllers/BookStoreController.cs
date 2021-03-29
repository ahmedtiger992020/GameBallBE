using Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Enums.Dtos;
using Core.UseCases.Lookup.TestApp;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BookStoreController : ControllerBase
    {
        private static IHubContext<NotificationHub> _messageHubContext;

        #region Props
        private IAddBookUseCase _addBookUseCase { get; set; }
        private IAuthorGetDDLUseCase _getAuthorDDL { get; set; }
        private IAddBookReviewUseCase _addBookReviewUseCase { get; set; }
        private IBookGetByIdUseCase _bookGetByIdUseCase { get; set; }
        private IBookGetAllUseCase _getAllUseCase { get; set; }
        private IBookGetDDLUseCase _getBookDDL { get; set; }


        private IConfiguration _configuration { get; set; }
        public OutputPort<ResultDto<bool>> Presenter { get; set; }
        public OutputPort<ResultDto<BookGetByIdOutputDto>> GetByIdPresenter { get; set; }
        public OutputPort<ListResultDto<BookGetAllOutputDto>> GetAllPresenter { get; set; }
        public OutputPort<ListResultDto<DDLDto>> GetDDLPresenter { get; set; }


        #endregion
        public BookStoreController(IHubContext<NotificationHub> messageHubContext, IConfiguration Configuration, IAddBookUseCase AddBookUseCase, IBookGetByIdUseCase BookGetByIdUseCase, IAddBookReviewUseCase AddBookReviewUseCase, IBookGetAllUseCase BookGetAllUseCase, IAuthorGetDDLUseCase AuthorGetDDL, IBookGetDDLUseCase BookGetDDLUseCase )
        {
            _configuration = Configuration;
            _addBookUseCase = AddBookUseCase;
            _messageHubContext = messageHubContext;
            _bookGetByIdUseCase = BookGetByIdUseCase;
            _addBookReviewUseCase = AddBookReviewUseCase;
            _getAllUseCase = BookGetAllUseCase;
            _getAuthorDDL = AuthorGetDDL;
            _getBookDDL = BookGetDDLUseCase;
        }
        [HttpPost]
        public async Task<ActionResult<ResultDto<bool>>> Add([FromBody] AddBookInputDto request)
        {
            Presenter = new OutputPort<ResultDto<bool>>();
            await _addBookUseCase.HandleUseCase(request, Presenter);

            return Presenter.Result;
        }
        [HttpPost]
        public async Task<ActionResult<ResultDto<bool>>> AddReview([FromBody] AddBookReviewInputDto request)
        {
            Presenter = new OutputPort<ResultDto<bool>>();
            await _addBookReviewUseCase.HandleUseCase(request, Presenter);
            if (request.BookReview?.Any() ?? default)
            {
                await _messageHubContext.Clients.All.SendAsync("send", $"new review in book");
            }
            return Presenter.Result;
        }
        [HttpPost]
        public async Task<ActionResult<ResultDto<BookGetByIdOutputDto>>> GetById(int request)
        {
            GetByIdPresenter = new OutputPort<ResultDto<BookGetByIdOutputDto>>();
            await _bookGetByIdUseCase.HandleUseCase(request, GetByIdPresenter);
            return GetByIdPresenter.Result;
        }
        [HttpPost]
        public async Task<ActionResult<ListResultDto<BookGetAllOutputDto>>> GetAll([FromBody] BookGetAllInputDto request)
        {
            GetAllPresenter = new OutputPort<ListResultDto<BookGetAllOutputDto>>();
            await _getAllUseCase.HandleUseCase(request, GetAllPresenter);
            return GetAllPresenter.Result;
        }
        [HttpGet]
        public async Task<ActionResult<ListResultDto<DDLDto>>> GetAuthorDDL()
        {
            GetDDLPresenter = new OutputPort<ListResultDto<DDLDto>>();
            await _getAuthorDDL.HandleUseCase(GetDDLPresenter);
            return GetDDLPresenter.Result;
        }
        [HttpGet]
        public async Task<ActionResult<ListResultDto<DDLDto>>> GetBookDDL()
        {
            GetDDLPresenter = new OutputPort<ListResultDto<DDLDto>>();
            await _getBookDDL.HandleUseCase(GetDDLPresenter);
            return GetDDLPresenter.Result;
        }

    }
}
