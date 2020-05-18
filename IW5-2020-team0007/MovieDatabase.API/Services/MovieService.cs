using AutoMapper;
using MovieDatabase.Data.Models.Common;
using MovieDatabase.Data.Models.Movies;
using MovieDatabase.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieDatabase.API.Services
{
    public class MovieService : IDisposable
    {
        private IMapper Mapper { get; }
        private MoviesRepository MoviesRepository { get; }

        public MovieService(IMapper mapper, MoviesRepository moviesRepository)
        {
            Mapper = mapper;
            MoviesRepository = moviesRepository;
        }

        public PaginatedData<SimpleMovie> GetMoviesList(MovieSearchRequest request)
        {
            var query = MoviesRepository.GetMovies(request.Name, request.GenreIds, request.Country, request.LengthFrom, request.LengthTo);
            return PaginatedData<SimpleMovie>.Create(query, request, entityList => Mapper.Map<List<SimpleMovie>>(entityList));
        }

        public Movie CreateMovie(CreateMovieRequest request)
        {
            var names = request.MovieNames?.ToDictionary(o => o.Lang, o => o.Name);

            var entity = MoviesRepository.CreateMovie(request.OriginalName, request.TitleImageUrl, request.Country, request.Length, request.Description, request.CreatedYear.Value,
                names, request.Actors, request.Directors, request.GenreIds);

            return Mapper.Map<Movie>(entity);
        }

        public Movie GetMovieDetail(long id)
        {
            var entity = MoviesRepository.GetMovieByID(id);
            return Mapper.Map<Movie>(entity);
        }

        public Movie UpdateMovie(long id, EditMovieRequest request)
        {
            var names = request.MovieNames?.ToDictionary(o => o.Lang, o => o.Name);

            var entity = MoviesRepository.UpdateMovie(id, request.OriginalName, request.TitleImageUrl, request.Country, request.Length, request.Description,
                request.CreatedYear, names, request.Actors, request.Directors, request.GenreIds);

            return Mapper.Map<Movie>(entity);
        }

        public bool DeleteMovie(long id)
        {
            if (!MoviesRepository.ExistsMovie(id))
                return false;

            MoviesRepository.DeleteMovie(id);
            return true;
        }

        public void Dispose()
        {
            MoviesRepository.Dispose();
        }
    }
}
