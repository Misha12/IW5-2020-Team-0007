using AutoMapper;
using MovieDatabase.Data.Enums;
using MovieDatabase.Data.Models.Common;
using MovieDatabase.Data.Models.Search;
using MovieDatabase.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public PaginatedData<SearchResult> Search(SearchRequest request)
        {
            var moviesQuery = Repository.SearchInMovies(request.Keyword);
            var personsQuery = Repository.SearchInPersons(request.Keyword);
            var usersQuery = Repository.SearchInUsers(request.Keyword);
            var ratingsQuery = Repository.SearchInRatings(request.Keyword);

            var result = new List<SearchResult>();
            result.AddRange(Mapper.Map<List<MovieSearchResult>>(moviesQuery).Select(o => new SearchResult() { Type = SearchResultType.Movie, MovieResult = o }));
            result.AddRange(Mapper.Map<List<PersonSearchResult>>(personsQuery).Select(o => new SearchResult() { Type = SearchResultType.Person, PersonResult = o }));
            result.AddRange(Mapper.Map<List<UserSearchResult>>(usersQuery).Select(o => new SearchResult() { Type = SearchResultType.User, UserResult = o }));
            result.AddRange(Mapper.Map<List<RatingSearchResult>>(ratingsQuery).Select(o => new SearchResult() { Type = SearchResultType.Rating, RatingResult = o }));

            return PaginatedData<SearchResult>.Create(result, request);
        }

        public void Dispose()
        {
            Repository.Dispose();
        }
    }
}
