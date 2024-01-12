using MediatR;
using MovieApi.Data.Models;
using MovieApi.Handlers;

namespace MovieApi.Queries
{
    public class GetMovieQuery : IRequest<HandlerResponse<List<Movie>>>
    {
    }
}
