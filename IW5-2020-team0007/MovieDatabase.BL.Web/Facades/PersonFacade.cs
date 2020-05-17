using MovieDatabase.Web;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MovieDatabase.BL.Web.Facades
{
    public class PersonFacade
    {
        private readonly HttpClient httpClient;
        public PersonFacade(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<PaginatedDataOfSimplePerson> GetPersonListAsync(String nameSurname, int? limit, int? page)
        {
            PersonsControllerClient _personClient = new PersonsControllerClient(httpClient);
            var a = await _personClient.GetPersonListAsync(nameSurname, limit, page);
            return a;
        }


        public async Task<SimplePerson> CreatePersonAsync(String token, CreatePersonRequest createPersonRequest)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            PersonsControllerClient _personClient = new PersonsControllerClient(httpClient);
            var a = await _personClient.CreatePersonAsync(createPersonRequest);
            return a;
        }

        public async Task<Person> GetPersonDetailAsync(String ID)
        {
            PersonsControllerClient _personClient = new PersonsControllerClient(httpClient);
            var a = await _personClient.GetPersonDetailAsync(long.Parse(ID));
            return a;
        }

        public async Task<Person> UpdatePersonAsync(String token,String ID, EditPersonRequest editPersonRequest)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            PersonsControllerClient _personClient = new PersonsControllerClient(httpClient);
            var a = await _personClient.UpdatePersonAsync(long.Parse(ID),editPersonRequest);
            return a;
        }

        public async Task DeletePersonAsync(String token, String ID)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            PersonsControllerClient _personClient = new PersonsControllerClient(httpClient);
            await _personClient.DeletePersonAsync(long.Parse(ID));
        }
    }
}
