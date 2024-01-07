using ExampleApi.Contexts;
using ExampleApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ExampleApi.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private ExampleApiContext _context;

    public CustomerRepository(ExampleApiContext context)
    {
        _context = context;
    }

    public async Task<Customer?> Create(Customer customer)
    {
        var result = await _context.Customers.AddAsync(customer);

        return result.Entity;
    }

    public Customer? Update(Customer customer)
    {
        var result = _context.Customers.Update(customer);

        return result.Entity;
    }

    public Customer? Delete(Customer customer)
    {
        var result = _context.Customers.Remove(customer);

        return result.Entity;
    }

    public async Task<IEnumerable<Customer>> FindAll()
    {
        return await _context.Customers
            .AsNoTracking()
            .Include(c => c.Individual)
            .Include(i => i.Individual.Address)
            .Include(i => i.Individual.Contact)
            .ToListAsync();
    }

    public async Task<Customer?> FindOne(int id)
    {
        return await _context.Customers
            .Include(c => c.Individual)
            .ThenInclude(i => i.Address)
            .Include(c => c.Individual)
            .ThenInclude(i => i.Contact)
            .SingleOrDefaultAsync(c => c.Id == id);
    }
}