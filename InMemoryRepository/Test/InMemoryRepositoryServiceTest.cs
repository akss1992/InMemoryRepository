using System;
using System.Threading.Tasks;
using InMemoryRepository.Model;
using InMemoryRepository.Service;
using Xunit;

namespace InMemoryRepository.Test
{
    public class InMemoryRepositoryServiceTest
    {
        private PersonRepositoryServiceImpl testService;
        private const string PERSON_NAME = "Arpan";
        private const string PERSON_EMAIL = "arpan@gmail.com";
        private const int PERSON_ID = 0;

        public InMemoryRepositoryServiceTest()
        {
            testService = new PersonRepositoryServiceImpl();           
        }

        [Fact]
        public async Task WhenAddAsyncThenNonZeroIntReturned()
        {
            int id = await CreatePersonAsync();
            Assert.NotEqual(0, id);
        }

        [Fact]
        public async Task WhenAddingTwoUniqueProductBothReturnsValuesUniqueAndNotEqualToZero()
        {
            int personAId = await CreatePersonAsync();
            int personBId = await CreatePersonAsync(name: "Tom", email: "tom@gmail.com"); 

            Assert.NotEqual(0, personAId);
            Assert.NotEqual(0, personAId);
            Assert.NotEqual(personAId, personAId);
        }

        [Fact]
        public async Task WhenGetByIdProductWithSameDetailsReturned()
        {
            int personId = await CreatePersonAsync();
            Person getPersonDetails = await testService.getPersonAsync(personId);

            Assert.NotNull(getPersonDetails);
            Assert.Equal(PERSON_NAME, getPersonDetails.name);
            Assert.Equal(PERSON_EMAIL, getPersonDetails.email);
            Assert.Equal(personId, getPersonDetails.id);
        }

        [Fact]
        public async Task WhenAddNewPersontWithSameEmailThrowsException()
        {
            await CreatePersonAsync();
            await Assert.ThrowsAnyAsync<InvalidOperationException>(() => CreatePersonAsync(name: "Mehul"));
        }

        [Fact]
        public Task WhenAddingNullPersonThrowsException()
            => Assert.ThrowsAnyAsync<ArgumentNullException>(() => testService.addPersonAsync(null));

        [Fact]
        public Task WhenAddingNullPersonNameThrowsException()
            => Assert.ThrowsAnyAsync<ArgumentNullException>(() => CreatePersonAsync(name: null));

        [Fact]
        public Task WhenAddingNullPersonEmailThrowsException()
            => Assert.ThrowsAnyAsync<ArgumentNullException>(() => CreatePersonAsync(email: null));

        [Fact]
        public Task WhenUpdatingNullPersonThrowsException()
            => Assert.ThrowsAnyAsync<ArgumentNullException>(() => testService.updatePersonAsync(0,null));

        [Fact]
        public async Task WhenUpdatedByIdPersonWithGivenDetailsReturned()
        {
            int personId = await CreatePersonAsync();
            Person updatePerson = new Person
            {
                name = "Tom",
                email = "Tom@gmail.com"
            };

            Person updatedPersonDetails = await testService.updatePersonAsync(personId, updatePerson);

            Assert.NotNull(updatedPersonDetails);
            Assert.Equal(updatePerson.name, updatedPersonDetails.name);
            Assert.Equal(updatePerson.email, updatedPersonDetails.email);
            Assert.Equal(personId, updatedPersonDetails.id);
        }

        [Fact]
        public async Task WhenUpdatingExistingPersontWithExisitingEmailThrowsException()
        {
            int personId = await CreatePersonAsync();
            await Assert.ThrowsAnyAsync<InvalidOperationException>(() => testService.updatePersonAsync(personId, new Person
            {
                name = "Tom",
                email = PERSON_EMAIL
            }));
        }

        private Task<int> CreatePersonAsync(string name = PERSON_NAME, string email = PERSON_EMAIL)
        => testService.addPersonAsync(new Person {
          name = name,
          email = email
        });

    }
}

