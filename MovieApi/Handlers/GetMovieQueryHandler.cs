using MediatR;
using MovieApi.Data.Interfaces;
using MovieApi.Data.Models;
using MovieApi.Queries;

namespace MovieApi.Handlers
{
    public class GetMovieQueryHandler : IRequestHandler<GetMovieQuery, HandlerResponse<List<Movie>>>
    {
        public IMovieRepository _movieRepository { get; set; }
        public ILogger<GetMovieQueryHandler> _logger { get; set; }
        public GetMovieQueryHandler(IMovieRepository movieRepository, ILogger<GetMovieQueryHandler> logger)
        {
            _movieRepository = movieRepository;
            _logger = logger;
        }
        public async Task<HandlerResponse<List<Movie>>> Handle(GetMovieQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _movieRepository.Get();

                return new HandlerResponse<List<Movie>>
                {
                    Success = true,
                    Data = response
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an issue with the request");
                return new HandlerResponse<List<Movie>>
                {
                    Success = false,
                    Message = "There was an issue with the request"
                };
            }
        }
    }
}
