using System;
using System.Threading.Tasks;
using InMemoryRepository.Model;

namespace InMemoryRepository.Service
{
    public interface InMemoryRepositoryService
    {
        Task<int> addPersonAsync(Person person);

        Task removePeronAsync(int id);

        Task updatePersonAsync(int id, Person person);

        Task<Person> getPersonAsync(int productId);
    }
}
