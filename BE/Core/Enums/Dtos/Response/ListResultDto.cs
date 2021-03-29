using System.Collections.Generic;

namespace Core.Enums.Dtos
{
    public class ListResultDto<T> : ResultBaseDto
    {
        public ListResultDto() { }
        public List<T> Data { get; set; }
        public int TotalCount { get; set; }

        public ListResultDto(List<T> data, int totalCount, bool isSuccess = true, string message = "") : base(isSuccess, message)
        {
            TotalCount = totalCount;
            Data = data;
        }
    }
}
