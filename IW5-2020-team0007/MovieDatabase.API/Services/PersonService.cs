using AutoMapper;
using MovieDatabase.Data.Models.Common;
using MovieDatabase.Data.Models.Persons;
using MovieDatabase.Data.Repository;
using System;
using System.Collections.Generic;

namespace MovieDatabase.API.Services
{
    public class PersonService : IDisposable
    {
        private PersonsRepository PersonsRepository { get; }
        private IMapper Mapper { get; }

        public PersonService(IMapper mapper, PersonsRepository personsRepository)
        {
            Mapper = mapper;
            PersonsRepository = personsRepository;
        }

        public PaginatedData<SimplePerson> GetPersonList(PersonSearchRequest request)
        {
            var query = PersonsRepository.GetPersons(request.NameSurname);
            return PaginatedData<SimplePerson>.Create(query, request, entityList => Mapper.Map<List<SimplePerson>>(entityList));
        }

        public SimplePerson CreatePerson(CreatePersonRequest request)
        {
            var entity = PersonsRepository.CreatePerson(request.Name, request.Surname, request.ProfilePictureUrl, request.Birthday);
            return Mapper.Map<SimplePerson>(entity);
        }

        public Person GetPersonDetail(long id)
        {
            var person = PersonsRepository.FindPersonById(id);
            return Mapper.Map<Person>(person);
        }

        public Person UpdatePerson(long id, EditPersonRequest request)
        {
            var person = PersonsRepository.UpdatePerson(id, request.Name, request.Surname, request.ProfilePictureUrl, request.Birthday);
            return Mapper.Map<Person>(person);
        }

        public void Dispose()
        {
            PersonsRepository.Dispose();
        }

        /*

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
        */
    }
}
