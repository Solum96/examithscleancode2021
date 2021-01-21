using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieLibrary.Controllers;
using MovieLibrary.Models;
using MovieLibrary.Services;
using System.Collections.Generic;

namespace MovieLibraryTests
{
    [TestClass]
    public class MovieControllerTest
    {
        [TestMethod]
        public void SortMoviesByRatingTest()
        {
            var movieList = new List<Movie>() {
                new Movie() { Title = "Lower Rating", Rating = "5.5" },
                new Movie() { Title = "Higher Rating", Rating = "8.9" },
                new Movie() { Title = "Middle Rating", Rating = "6.5" },
                new Movie() { Title = "B:KommerNäst", Rating = "2.5"},
                new Movie() { Title = "A:KommerFörst", Rating = "2.5"}
            };
            var service = new MovieService();

            var expected = new List<Movie>() {
                new Movie() { Title = "Higher Rating", Rating = "8.9" },
                new Movie() {Title = "Middle Rating", Rating = "6.5" },
                new Movie() { Title = "Lower Rating", Rating = "5.5" },
                new Movie() { Title = "A:KommerFörst", Rating = "2.5"},
                new Movie() { Title = "B:KommerNäst", Rating = "2.5"}
            };
            var actual = service.SortMoviesByRating(movieList, false);

            Assert.AreEqual(expected.Count, actual.Count);
            foreach (var movie in actual)
            {
                Assert.AreEqual(expected[actual.IndexOf(movie)].Title, movie.Title);
            }
        }

        [TestMethod]
        public void FindMovieByIdTest()
        {
            var movieList = new List<Movie>() {
                new Movie() { Title = "Title", Rating = "5.5", Id = "2"},
                new Movie() { Title = "Don't find this", Rating = "8.9", Id = "0"},
                new Movie() { Title = "Find this", Rating = "6.5", Id = "1" },
            };
            var service = new MovieService();

            var expected = new Movie() { Title = "Find this", Rating = "6.5", Id = "1" };
            var actual = service.FindMovieWithId(movieList, "1");

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Title, actual.Title);
        }

        [TestMethod]
        public void CombineMovieListsTest()
        {
            var movieList1 = new List<Movie>() {
                new Movie() { Title = "Unique1", Rating = "5.5", Id = "2"},
                new Movie() { Title = "Unique2", Rating = "8.9", Id = "0"},
                new Movie() { Title = "ShouldBeOnlyOne", Rating = "6.5", Id = "1" },
            };

            var movieList2 = new List<Movie>() {
                new Movie() { Title = "Unique3", Rating = "5.5", Id = "2"},
                new Movie() { Title = "Unique4", Rating = "8.9", Id = "0"},
                new Movie() { Title = "ShouldBeOnlyOne", Rating = "6.5", Id = "1" },
            };
            var service = new MovieService();

            var expected = 5;
            var actual = service.CombineMovieListsWithoutDuplicates(movieList1, movieList2).Count;

            Assert.AreEqual(expected, actual);
        }
    }
}
