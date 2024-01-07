using ExampleApi.Contexts;
using ExampleApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ExampleApi.Repositories;

public class AddressRepository : IAddressRepository
{
    private ExampleApiContext _context;

    public AddressRepository(ExampleApiContext context)
    {
        _context = context;
    }

    public async Task<Address?> Create(Address address)
    {
        var result = _context.Addresses != null ? await _context.Addresses.AddAsync(address) : null;

        return result?.Entity;
    }

    public Address? Update(Address address)
    {
        var result = _context.Addresses?.Update(address);

        return result?.Entity;
    }

    public Address? Delete(Address address)
    {
        var result = _context.Addresses?.Remove(address);

        return result?.Entity;
    }

    public async Task<IEnumerable<Address>> FindAll()
    {
        return _context.Addresses != null ? await _context.Addresses.AsNoTracking().ToListAsync() : [];
    }

    public async Task<Address?> FindOne(int id)
    {
        return _context.Addresses != null ? await _context.Addresses.AsNoTracking().SingleOrDefaultAsync(a => a.Id == id) : null;
    }
}