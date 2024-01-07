using ExampleApi.Models;

namespace ExampleApi.Repositories;

public interface IAddressRepository
{
    Task<IEnumerable<Address>> FindAll();
    Task<Address?> FindOne(int id);
    Task<Address?> Create(Address address);
    Address? Update(Address address);
    Address? Delete(Address address);
}