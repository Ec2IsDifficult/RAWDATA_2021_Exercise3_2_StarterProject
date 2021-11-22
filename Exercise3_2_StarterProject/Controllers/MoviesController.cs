using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataServiceLayer;
using Exercise3_2_StarterProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Exercise3_2_StarterProject.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MoviesController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public MoviesController(IDataService dataService, LinkGenerator linkGenerator, IMapper mapper)
        {
            _dataService = dataService;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
        }

        [HttpGet(Name = nameof(GetMovies))]
        public IActionResult GetMovies(int page = 0, int pageSize = 10)
        {
            var movies = _dataService.GetMovies(page, pageSize).Select(CreateDto);
            var total = _dataService.NumberOfMovies();
            var pages = Math.Ceiling(total / (double)pageSize);
            var prev = page > 0 
                ? _linkGenerator.GetUriByName(HttpContext, nameof(GetMovies), new { page = page - 1, pageSize }) 
                : null;
            var next = page < pages - 1 
                ? _linkGenerator.GetUriByName(HttpContext, nameof(GetMovies), new { page = page + 1, pageSize }) 
                : null;

            var result = new
            {
                total,
                pages,
                prev,
                next,
                items = movies
            };

            return Ok(result);
        }

        [HttpGet("{id}", Name = nameof(GetMovie))]
        public IActionResult GetMovie(string id)
        {
            var movie = _dataService.GetMovie(id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(CreateDto(movie));
        }

        /**
         *
         * Helpers
         *
         */

        private MovieDto CreateDto(Movie movie)
        {
            var dto = _mapper.Map<MovieDto>(movie);
            dto.Url = _linkGenerator.GetUriByName(HttpContext, nameof(GetMovie), new {movie.Id});
            return dto;
        }
    }
}
