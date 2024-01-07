using ExampleApi.Contexts;
using ExampleApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ExampleApi.Repositories;

public class IndividualRepository : IIndividualRepository
{
    private ExampleApiContext _context;

    public IndividualRepository(ExampleApiContext context)
    {
        _context = context;
    }

    public async Task<Individual?> Create(Individual individual)
    {
        var result = await _context.Individuals.AddAsync(individual);

        return result.Entity;
    }

    public Individual? Update(Individual individual)
    {
        var result = _context.Individuals.Update(individual);

        return result.Entity;
    }

    public Individual? Delete(Individual individual)
    {
        var result = _context.Individuals.Remove(individual);

        return result.Entity;
    }

    public async Task<IEnumerable<Individual>> FindAll()
    {
        return await _context.Individuals.AsNoTracking().ToListAsync();
    }

    public async Task<Individual?> FindOne(int id)
    {
        return await _context.Individuals.AsNoTracking().SingleOrDefaultAsync(a => a.Id == id);
    }
}