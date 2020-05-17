using AutoMapper;
using System;
using MovieDatabase.Data.Repository;
using MovieDatabase.Data.Models.Ratings;
using MovieDatabase.Data.Models.Common;
using System.Collections.Generic;
using System.Security.Claims;
using MovieDatabase.Common.Extensions;
using System.Linq;
using MovieDatabase.Data.Enums;

namespace MovieDatabase.API.Services
{
    public class RatesService : IDisposable
    {
        private MoviesRepository MoviesRepository { get; }
        private IMapper Mapper { get; }

        public RatesService(IMapper mapper, MoviesRepository moviesRepository)
        {
            Mapper = mapper;
            MoviesRepository = moviesRepository;
        }

        public Rating CreateRate(CreateRateRequest request, long loggedUserID)
        {
            var entity = MoviesRepository.CreateRate(request.MovieID, request.Score, request.Text, loggedUserID, request.Anonymous);

            return Mapper.Map<Rating>(entity);
        }

        public PaginatedData<Rating> GetRateList(RatingSearchRequest request)
        {
            var query = MoviesRepository.GetRatesList(request.MovieIDs, request.TextContains, request.ScoreFrom, request.ScoreTo);
            return PaginatedData<Rating>.Create(query, request, entityList => Mapper.Map<List<Rating>>(entityList));
        }

        public Rating UpdateRate(long id, EditRatingRequest request, ClaimsPrincipal currentLoggedUser)
        {
            var loggedUserID = currentLoggedUser.GetUserID();
            var isAdmin = new[] { Roles.ContentManager.ToString(), Roles.Administrator.ToString() }.Contains(currentLoggedUser.GetUserRole());

            var entity = MoviesRepository.EditRate(id, request.Text, request.NewMovieID, request.Score, request.Anonymous, loggedUserID, isAdmin);
            return Mapper.Map<Rating>(entity);
        }

        public bool DeleteRate(long id, ClaimsPrincipal currentLoggedUser)
        {
            var loggedUserID = currentLoggedUser.GetUserID();
            var isCurrentLoggedUserAdmin = new[] { Roles.ContentManager.ToString(), Roles.Administrator.ToString() }.Contains(currentLoggedUser.GetUserRole());

            var rate = MoviesRepository.GetRateById(id);

            if (rate == null)
                return false;

            if (rate.UserID != loggedUserID && !isCurrentLoggedUserAdmin)
                throw new UnauthorizedAccessException();

            MoviesRepository.DeleteRate(id);
            return true;
        }

        public void Dispose()
        {
            MoviesRepository.Dispose();
        }
    }
}
;