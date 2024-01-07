using ExampleApi.Contexts;
using ExampleApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ExampleApi.Repositories;

public class ContactRepository : IContactRepository
{
    private ExampleApiContext _context;

    public ContactRepository(ExampleApiContext context)
    {
        _context = context;
    }

    public async Task<Contact?> Create(Contact contact)
    {
        var result = await _context.Contacts.AddAsync(contact);

        return result.Entity;
    }

    public Contact? Update(Contact contact)
    {
        var result = _context.Contacts.Update(contact);

        return result.Entity;
    }

    public Contact? Delete(Contact contact)
    {
        var result = _context.Contacts.Remove(contact);

        return result.Entity;
    }

    public async Task<IEnumerable<Contact>> FindAll()
    {
        return await _context.Contacts.AsNoTracking().ToListAsync();
    }

    public async Task<Contact?> FindOne(int id)
    {
        return await _context.Contacts.AsNoTracking().SingleOrDefaultAsync(c => c.Id == id);
    }
}