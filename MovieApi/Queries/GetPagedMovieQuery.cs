using MediatR;
using MovieApi.Data.Models;
using MovieApi.Handlers;

namespace MovieApi.Queries
{
    public class GetPagedMovieQuery : IRequest<HandlerResponse<List<Movie>>>
    {
        public GetPagedMovieQuery(int page, int pageSize, string? filterValue)
        {
            Page = page;
            PageSize = pageSize;
            FilterValue = filterValue;
        }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string? FilterValue { get; set; }
    }
}
