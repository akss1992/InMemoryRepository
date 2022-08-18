using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InMemoryRepository.Model;

namespace InMemoryRepository.Service
{
    public class PersonRepositoryServiceImpl : InMemoryRepositoryService
    {
        private int _LastIdentifier = 0;
        List<Person> _Persons;
        public PersonRepositoryServiceImpl()
        {
            _Persons = new List<Person>(); 
        }

        public Task<int> addPersonAsync(Person person)
        {
            if (person == null)
                throw new ArgumentNullException(nameof(person));

            if (string.IsNullOrWhiteSpace(person.name))
                throw new ArgumentException("Person name cannot be null or whitespace.", nameof(person));

            if (string.IsNullOrWhiteSpace(person.email))
                throw new ArgumentException("Person email cannot be null or whitespace.", nameof(person));

            if (_Persons.Any((p) => p.email.Equals(person.email, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException($"Product with name {person.email} already exists.");

            return Task.FromResult(++_LastIdentifier);
        }

        public Task<Person> getPersonAsync(int personId)
        {
            List<Person> person = new List<Person>();
            Person getPersonDetails = person.FirstOrDefault((p) => p.id == personId);
            return Task.FromResult(getPersonDetails);
        }

        public Task<Person> updatePersonAsync(int personId, Person person)
        {
            if (person == null)
                throw new ArgumentNullException(nameof(person));

            if (string.IsNullOrWhiteSpace(person.name))
                throw new ArgumentException("Person name cannot be null or whitespace.", nameof(person));

            if (string.IsNullOrWhiteSpace(person.email))
                throw new ArgumentException("Person email cannot be null or whitespace.", nameof(person));

            if (_Persons.Any((p) => p.email.Equals(person.email, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException($"Product with name {person.email} already exists.");

            List<Person> updatedPerson = new List<Person>();
            Person updatedPersonDetails = updatedPerson.FirstOrDefault((p) => p.id == personId);

            return Task.FromResult(updatedPersonDetails);
        }

        public Task removePeronAsync(int id)
        {
            if(!_Persons.Exists((p) => p.id.Equals(id)))
                throw new InvalidOperationException($"Pesron with id {id} already exists.");
            throw new NotImplementedException();
        }

        Task InMemoryRepositoryService.updatePersonAsync(int id, Person person)
        {
            throw new NotImplementedException();
        }
    }
}
