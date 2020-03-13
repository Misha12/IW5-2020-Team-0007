using MovieDatabase.Domain;
using MovieDatabase.Domain.DTO;
using System.Collections.Generic;
using System.Linq;
using DBPerson = MovieDatabase.Domain.Entity.Person;

namespace MovieDatabase.API.Services
{
    public class PersonManagementService
    {
        private MovieDatabaseContext Context { get; }
        public PersonManagementService(MovieDatabaseContext context)
        {
            Context = context;
        }
        public Person FindPersonById(int id)
        {
            var item = Context.Persons.FirstOrDefault(o => o.ID == id);

            if (item == null)
                return null;

            return new Person()
            {
                ID = item.ID,
                Name = item.Name,
                Surname = item.Surname,
                Birthdate = item.Birthdate,
                ProfilePicture = item.ProfilePicture


            };
        }


    }
}