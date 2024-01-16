using MediatR;
using MovieApi.Data.Interfaces;
using MovieApi.Data.Models;
using MovieApi.Queries;

namespace MovieApi.Handlers
{
    public class GetPagedMovieQueryHandler : IRequestHandler<GetPagedMovieQuery, HandlerResponse<List<Movie>>>
    {
        public IMovieRepository _movieRepository { get; set; }
        public ILogger<GetPagedMovieQueryHandler> _logger { get; set; }
        public GetPagedMovieQueryHandler(IMovieRepository movieRepository, ILogger<GetPagedMovieQueryHandler> logger)
        {
            _movieRepository = movieRepository;
            _logger = logger;
        }
        public async Task<HandlerResponse<List<Movie>>> Handle(GetPagedMovieQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var totalCount = await _movieRepository.Get();
                var response = await _movieRepository.GetByPageNumber(request.Page, request.PageSize, request.FilterValue);

                return new HandlerResponse<List<Movie>>
                {
                    Success = true,
                    Data = response,
                    Count = totalCount.Count,
                    PageIndex = request.Page,
                    PageSize = request.PageSize,
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
