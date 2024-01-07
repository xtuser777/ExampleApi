using ExampleApi.Models;

namespace ExampleApi.Repositories;

public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> FindAll();
    Task<Customer?> FindOne(int id);
    Task<Customer?> Create(Customer customer);
    Customer? Update(Customer customer);
    Customer? Delete(Customer customer);
}