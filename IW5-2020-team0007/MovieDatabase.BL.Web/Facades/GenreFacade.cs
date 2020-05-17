using MovieDatabase.Web;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MovieDatabase.BL.Web.Facades
{
    public class GenreFacade
    {
        private readonly HttpClient httpClient;
        public GenreFacade(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<ICollection<Genre>> GetGenresListAsync(String search)
        {
            GenresControllerClient _genreClient = new GenresControllerClient(httpClient);
            var a = await _genreClient.GetGenresListAsync(search);
            return a;
        }

        public async Task<SimpleGenre> CreateGenreAsync(String token, CreateGenreRequest createGenreRequest)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            GenresControllerClient _genreClient = new GenresControllerClient(httpClient);
            var a = await _genreClient.CreateGenreAsync(createGenreRequest);
            return a;
        }

        public async Task<Genre> UpdateGenreAsync(String token,int ID, EditGenreRequest editGenreRequest)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            GenresControllerClient _genreClient = new GenresControllerClient(httpClient);
            var a = await _genreClient.UpdateGenreAsync(ID, editGenreRequest);
            return a;
        }

        public async Task DeleteGenreAsync(String token, int ID)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            GenresControllerClient _genreClient = new GenresControllerClient(httpClient);
            await _genreClient.DeleteGenreAsync(ID);
        }
    }
}
