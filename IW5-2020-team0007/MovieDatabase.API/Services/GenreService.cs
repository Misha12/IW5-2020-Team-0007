using AutoMapper;
using MovieDatabase.Data.Models.Genres;
using MovieDatabase.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieDatabase.API.Services
{
    public class GenreService : IDisposable
    {
        private GenresRepository GenresRepository { get; }
        private IMapper Mapper { get; }

        public GenreService(GenresRepository repository, IMapper mapper)
        {
            Mapper = mapper;
            GenresRepository = repository;
        }

        public List<Genre> GetFullList(GenresSearchRequest request)
        {
            var genres = GenresRepository.GetGenreList(request.Search).ToList();
            return Mapper.Map<List<Genre>>(genres);
        }

        public SimpleGenre CreateGenre(CreateGenreRequest request)
        {
            var genre = GenresRepository.CreateGenre(request.Name);
            return Mapper.Map<SimpleGenre>(genre);
        }

        public Genre UpdateGenre(int id, EditGenreRequest request)
        {
            var updatedGenre = GenresRepository.UpdateGenre(id, request.Name);

            if (updatedGenre == null)
                return null;

            return Mapper.Map<Genre>(updatedGenre);
        }

        public bool DeleteGenre(int id)
        {
            if (!GenresRepository.GenreExists(id))
                return false;

            GenresRepository.DeleteGenre(id);
            return true;
        }

        public void Dispose()
        {
            GenresRepository.Dispose();
        }
    }
}
