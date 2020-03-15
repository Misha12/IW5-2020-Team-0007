using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieDatabase.API.Models;
using MovieDatabase.Domain;
using MovieDatabase.Domain.DTO;
using System.Collections.Generic;
using System.Linq;
using DBPerson = MovieDatabase.Domain.Entity.Person;
using MoviePersonType = MovieDatabase.Domain.Entity.MoviePersonType;

namespace MovieDatabase.API.Services
{
    public class PersonService
    {
        private MovieDatabaseContext Context { get; }
        private IMapper Mapper { get; }

        public PersonService(MovieDatabaseContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        public List<Person> GetPersons(string search = null)
        {
            var query = Context.Persons.AsQueryable();

            if (!string.IsNullOrEmpty(search))
                query = query.Where(o => o.Name.Contains(search) || o.Surname.Contains(search));

            var data = query.ToList();
            return Mapper.Map<List<Person>>(data);
        }

        public PersonDetail FindPersonByID(long id)
        {
            var person = Context.Persons
                .Include(o => o.InMovies)
                .ThenInclude(o => o.Movie)
                .FirstOrDefault(o => o.ID == id);

            var mapped = Mapper.Map<PersonDetail>(person);

            if(person.InMovies?.Count > 0)
            {
                mapped.ActingIn = person.InMovies.Where(o => o.Type == MoviePersonType.Actor).Select(o => Mapper.Map<Movie>(o.Movie)).ToList();
                mapped.DirectedIn = person.InMovies.Where(o => o.Type == MoviePersonType.Director).Select(o => Mapper.Map<Movie>(o.Movie)).ToList();
            }

            return mapped;
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

            return Mapper.Map<Person>(entity);
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
            return Mapper.Map<Person>(person);
        }
    }
}
