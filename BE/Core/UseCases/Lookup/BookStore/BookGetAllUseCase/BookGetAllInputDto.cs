
using Core.Enums.Dtos;

namespace Core.UseCases.Lookup.TestApp
{
    public class BookGetAllInputDto
    {
        public PagingModel Paging { get; set; }
        public SortingModel SortingModel { get; set; }
        public string Name { get; set; }
    }
}
