using MovieDatabase.Web;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MovieDatabase.BL.Web.Facades
{
    public class MovieFacade
    {
        private readonly HttpClient httpClient;
        public MovieFacade(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<Movie> CreateMovieAsync (String token, CreateMovieRequest movie)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            MoviesControllerClient _movieClient = new MoviesControllerClient(httpClient);
            var a = await _movieClient.CreateMovieAsync(movie);
            return a;
        }

        public async Task<PaginatedDataOfSimpleMovie> GetMoviesListAsync(String token,String name,IEnumerable<int> genresIds, String country, long? lengthFrom, long? lengthTo, int? limit, int? page)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            MoviesControllerClient _movieClient = new MoviesControllerClient(httpClient);
            var a = await _movieClient.GetMoviesListAsync(name, genresIds, country, lengthFrom, lengthTo, limit, page);
            return a;
        }

        public async Task<Movie> GetMovieDetailAsync(String token, long ID)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            MoviesControllerClient _movieClient = new MoviesControllerClient(httpClient);
            var a = await _movieClient.GetMovieDetailAsync(ID);
            return a;
        }

        public async Task<Movie> UpdateMovieAsync(String token, long ID, EditMovieRequest newMovie)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            MoviesControllerClient _movieClient = new MoviesControllerClient(httpClient);
            var a = await _movieClient.UpdateMovieAsync(ID, newMovie);
            return a;
        }

        public async Task DeleteMovieAsync(String token, long ID)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            MoviesControllerClient _movieClient = new MoviesControllerClient(httpClient);
            await _movieClient.DeleteMovieAsync(ID);
        }
    }
}
