using System.Collections.Generic;

namespace Core.Enums.Dtos
{
    public class ResultDto<T> : ResultBaseDto
    {
        public  ResultDto()
        { }
        public T Data { get; set; }

        public ResultDto(T data, bool isSuccess = true, string message = "") : base(isSuccess, message)
        {
            Data = data;
        }
    }
}
