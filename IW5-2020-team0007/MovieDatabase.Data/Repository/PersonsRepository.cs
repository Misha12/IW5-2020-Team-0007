using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieDatabase.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieDatabase.Data.Repository
{
    public class PersonsRepository : RepositoryBase
    {
        public PersonsRepository(AppDbContext context) : base(context)
        {
        }

        private IQueryable<Person> GetQuery(bool includeMovies)
        {
            var query = Context.Persons.AsQueryable();

            if (includeMovies)
                query = query.Include(o => o.Movies);

            return query;
        }

        public bool AllPersonsExists(List<long> personIds)
        {
            var persons = GetQuery(false)
                .Where(o => personIds.Contains(o.ID))
                .Select(o => o.ID)
                .ToList();

            foreach (var id in personIds)
            {
                if (!persons.Contains(id))
                    return false;
            }

            return true;
        }

        public Person CreatePerson(string name, string surname, string profilePictureUrl, DateTime birthday)
        {
            var entity = new Person()
            {
                Birthdate = birthday,
                Name = name,
                Surname = surname,
                ProfilePictureUrl = profilePictureUrl
            };

            Context.Persons.Add(entity);
            Context.SaveChanges();

            return entity;
        }

        public IQueryable<Person> GetPersons(string nameSurnameQuery)
        {
            var query = GetQuery(false);

            if (!string.IsNullOrEmpty(nameSurnameQuery))
                query = query.Where(o => (o.Name + " " + o.Surname).Contains(nameSurnameQuery));

            return query;
        }

        public Person FindPersonById(long id)
        {
            return GetQuery(true)
                .SingleOrDefault(o => o.ID == id);
        }

        public Person UpdatePerson(long id, string name, string surname, string profilePictureUrl, DateTime? birthday)
        {
            var person = FindPersonById(id);

            if (person == null)
                return null;

            if (!string.IsNullOrEmpty(name))
                person.Name = name;

            if (!string.IsNullOrEmpty(surname))
                person.Surname = surname;

            if (!string.IsNullOrEmpty(profilePictureUrl))
                person.ProfilePictureUrl = profilePictureUrl;

            if (birthday != null)
                person.Birthdate = birthday.Value;

            Context.SaveChanges();
            return person;
        }
    }
}
