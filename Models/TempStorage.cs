using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCollection.Models
{
    //this is a model that stores the movie list. 
    public static class TempStorage
    {
        private static List<Movie> movies = new List<Movie>();

        public static IEnumerable<Movie> movieList => movies;

        public static void AddMovie(Movie movie)
        {
            movies.Add(movie);
        }
    }
}
