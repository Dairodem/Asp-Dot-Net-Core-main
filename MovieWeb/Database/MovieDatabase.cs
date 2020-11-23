using MovieWeb.Models;
using System.Collections.Generic;
using System.Linq;

namespace MovieWeb.Database
{
    public interface IMovieDatabase
    {
        Movie Insert(Movie movie);
        IEnumerable<Movie> GetMovies();
        Movie GetMovie(int id);
        void Delete(int id);
        void Update(int id, Movie movie);
    }

    public class MovieDatabase : IMovieDatabase
    {
        private readonly MovieDbContext _movies;

        public MovieDatabase(MovieDbContext dbContext)
        {
            _movies = dbContext;
        }

        public Movie GetMovie(int id)
        {
            return _movies.Movies.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Movie> GetMovies()
        {
            return _movies.Movies;
        }

        public Movie Insert(Movie movie)
        {
            _movies.Movies.Add(movie);
            _movies.SaveChanges();
            return movie;
        }

        public void Delete(int id)
        {
            var movie = _movies.Movies.SingleOrDefault(x => x.Id == id);
            if (movie != null)
            {
                _movies.Movies.Remove(movie);
                _movies.SaveChanges();
            }
        }

        public void Update(int id, Movie updatedMovie)
        {
            var movie = _movies.Movies.SingleOrDefault(x => x.Id == id);
            if (movie != null)
            {
                movie.Title = updatedMovie.Title;
                movie.Description = updatedMovie.Description;
                movie.ReleaseDate = updatedMovie.ReleaseDate;
                movie.Rating = updatedMovie.Rating;
                movie.Genre = updatedMovie.Genre;

                _movies.SaveChanges();
            }
        }
    }
}