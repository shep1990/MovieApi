using MediatR;
using MovieApi.Data.Models;
using MovieApi.Handlers;

namespace MovieApi.Queries
{
    public class GetPagedMovieQuery : IRequest<HandlerResponse<List<Movie>>>
    {
        public GetPagedMovieQuery(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
