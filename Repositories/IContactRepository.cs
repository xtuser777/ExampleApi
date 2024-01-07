using ExampleApi.Models;

namespace ExampleApi.Repositories;

public interface IContactRepository
{
    Task<IEnumerable<Contact>> FindAll();
    Task<Contact?> FindOne(int id);
    Task<Contact?> Create(Contact contact);
    Contact? Update(Contact contact);
    Contact? Delete(Contact contact);
}