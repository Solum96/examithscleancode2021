using MovieLibrary.Controllers;
using MovieLibrary.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;

namespace MovieLibrary.Services
{
    public class MovieService
    {
        HttpClient client = new HttpClient();
        public List<Movie> SortMoviesByRating(List<Movie> movies, bool sortAsAscending)
        {
            if (sortAsAscending) return movies.OrderBy(movie => movie.Rating).ThenBy(movie => movie.Title).ToList();
            else return movies.OrderByDescending(movie => movie.Rating).ThenBy(movie => movie.Title).ToList();
        }

        public Movie FindMovieWithId(List<Movie> movieList, string id)
        {
            return movieList.Select(i => i).Where(i => i.Id == id).FirstOrDefault();
        }

        public List<Movie> GetMovies()
        {
            var movieList1 = GetMoviesFromStandardApi();
            var movieList2 = GetMoviesFromDetailedApi();
            return CombineMovieListsWithoutDuplicates(movieList1, movieList2);
        }

        private List<Movie> GetMoviesFromStandardApi()
        {
            var clientResponse = client.GetAsync("https://ithstenta2020.s3.eu-north-1.amazonaws.com/topp100.json").Result;
            var movieList1 = JsonSerializer.Deserialize<List<Movie>>(new StreamReader(clientResponse.Content.ReadAsStream()).ReadToEnd());
            return movieList1;
        }

        private List<Movie> GetMoviesFromDetailedApi()
        {
            var clientResponse = client.GetAsync("https://ithstenta2020.s3.eu-north-1.amazonaws.com/detailedMovies.json").Result;
            var responseContent = JsonSerializer.Deserialize<List<ImdbMovie>>(new StreamReader(clientResponse.Content.ReadAsStream()).ReadToEnd());
            var movieList = responseContent.Select(i => new Movie() { Title = i.Title, Id = i.Id, Rating = i.Rating.ToString() }).ToList();
            return movieList;
        }

        public List<Movie> CombineMovieListsWithoutDuplicates(List<Movie> list1, List<Movie> list2)
        {
            var titleList1 = list1.Select(i => i.Title).ToList();

            foreach (var item in list2)
            {
                if (!titleList1.Contains(item.Title)) list1.Add(item);
            }
            return list1;
        }
    }
}
