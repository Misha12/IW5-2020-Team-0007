using Microsoft.EntityFrameworkCore;
using MovieDatabase.API.Models;
using MovieDatabase.Domain;
using MovieDatabase.Domain.DTO;
using System.Collections.Generic;
using System.Linq;
using DBPerson = MovieDatabase.Domain.Entity.Person;

namespace MovieDatabase.API.Services
{
    public class PersonService
    {
        private MovieDatabaseContext Context { get; }

        public PersonService(MovieDatabaseContext context)
        {
            Context = context;
        }

        public List<Person> GetPersons(string search = null)
        {
            var query = Context.Persons.AsQueryable();

            if (!string.IsNullOrEmpty(search))
                query = query.Where(o => o.Name.Contains(search) || o.Surname.Contains(search));

            var data = query.ToList();
            return data.Select(o => new Person(o)).ToList();
        }

        public PersonDetail FindPersonByID(long id)
        {
            var person = Context.Persons
                .Include(o => o.InMovies)
                .ThenInclude(o => o.Movie)
                .FirstOrDefault(o => o.ID == id);

            return person == null ? null : new PersonDetail(person);
        }

        public Person CreatePerson(PersonInput data)
        {
            var entity = new DBPerson()
            {
                Name = data.Name,
                ProfilePicture = data.ProfilePictureUrl,
                Surname = data.Surname
            };

            Context.Persons.Add(entity);
            Context.SaveChanges();

            return new Person(entity);
        }

        public bool DeletePerson(long id)
        {
            var person = Context.Persons.Include(o => o.InMovies).FirstOrDefault(o => o.ID == id);

            if (person == null)
                return false;

            person.InMovies.Clear();
            Context.Persons.Remove(person);
            Context.SaveChanges();

            return true;
        }

        public Person UpdatePerson(long id, PersonInput data)
        {
            var person = Context.Persons.FirstOrDefault(o => o.ID == id);

            if (person == null)
                return null;

            if (!string.IsNullOrEmpty(data.Name))
                person.Name = data.Name;

            if (!string.IsNullOrEmpty(data.Surname))
                person.Surname = data.Surname;

            if (!string.IsNullOrEmpty(data.ProfilePictureUrl))
                person.ProfilePicture = data.ProfilePictureUrl;

            Context.SaveChanges();
            return new Person(person);
        }
    }
}
