﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Queries;

namespace MovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly ILogger<MovieController> _logger;
        private readonly IMediator _mediator;
        public MovieController(ILogger<MovieController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;           
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetMovieQuery());
            return Ok(result);
        }

        [HttpGet("FilteredMovies")]
        public async Task<IActionResult> GetFilteredMovies(int page, int pageSize = 10, string? filterValue = null)
        {
            var pagedMovieQuery = new GetPagedMovieQuery(page, pageSize, filterValue);
            var result = await _mediator.Send(pagedMovieQuery);
            return Ok(result);
        }
    }
}
