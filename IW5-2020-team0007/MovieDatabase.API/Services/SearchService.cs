using AutoMapper;
using MovieDatabase.Data.Models.Common;
using MovieDatabase.Data.Models.Search;
using MovieDatabase.Data.Repository;
using System;
using System.Collections.Generic;

namespace MovieDatabase.API.Services
{
    public class SearchService : IDisposable
    {
        private IMapper Mapper { get; }
        private SearchRepository Repository { get; }

        public SearchService(IMapper mapper, SearchRepository repository)
        {
            Mapper = mapper;
            Repository = repository;
        }

        public PaginatedData<SearchResultBase> Search(SearchRequest request)
        {
            var moviesQuery = Repository.SearchInMovies(request.Keyword);
            var personsQuery = Repository.SearchInPersons(request.Keyword);
            var usersQuery = Repository.SearchInUsers(request.Keyword);
            var ratingsQuery = Repository.SearchInRatings(request.Keyword);

            var result = new List<SearchResultBase>();
            result.AddRange(Mapper.Map<List<MovieSearchResult>>(moviesQuery));
            result.AddRange(Mapper.Map<List<PersonSearchResult>>(personsQuery));
            result.AddRange(Mapper.Map<List<UserSearchResult>>(usersQuery));
            result.AddRange(Mapper.Map<List<RatingSearchResult>>(ratingsQuery));

            return PaginatedData<SearchResultBase>.Create(result, request);
        }

        public void Dispose()
        {
            Repository.Dispose();
        }
    }
}
