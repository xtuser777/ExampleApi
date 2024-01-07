using System.Linq.Expressions;
using ExampleApi.Contexts;
using ExampleApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ExampleApi.Repositories;

public class UserRepository : IUserRepository
{
    private ExampleApiContext _context;

    public UserRepository(ExampleApiContext context)
    {
        _context = context;
    }

    public async Task<User?> Create(User user)
    {
        var result = await _context.Users.AddAsync(user);

        return result.Entity;
    }

    public User? Update(User user)
    {
        var result = _context.Users.Update(user);

        return result.Entity;
    }

    public User? Delete(User user)
    {
        var result = _context.Users.Remove(user);

        return result.Entity;
    }

    public async Task<IEnumerable<User>> FindAll()
    {
        return await _context.Users
            .AsNoTracking()
            .Include(c => c.Individual)
            .Include(i => i.Individual.Address)
            .Include(i => i.Individual.Contact)
            .ToListAsync();
    }

    public async Task<User?> FindOne(Expression<Func<User?, bool>> predicate)
    {
        return await _context.Users
            .Include(c => c.Individual)
            .ThenInclude(i => i.Address)
            .Include(c => c.Individual)
            .ThenInclude(i => i.Contact)
            .SingleOrDefaultAsync(predicate);
    }
}