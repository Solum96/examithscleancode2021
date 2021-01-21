using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Services;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using MovieLibrary.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MovieLibrary.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class MovieController
    {
        static HttpClient client = new HttpClient();
        MovieService service = new MovieService();

        [HttpGet]
        [Route("/toplist")]
        public IEnumerable<string> GetMovieTitles(bool sortAsAscending = true)
        {
            var movieList = service.GetMovies();
            movieList = service.SortMoviesByRating(movieList, sortAsAscending);
            return movieList.Select(i => i.Title).ToArray();
        }

        [HttpGet]
        [Route("/movie")]
        public Movie GetMovieById(string id)
        {
            var movieList = service.GetMovies();
            var movie = service.FindMovieWithId(movieList, id);
            return movie;
        }
    }
}