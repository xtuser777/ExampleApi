using ExampleApi.Models;

namespace ExampleApi.Repositories;

public interface IIndividualRepository
{
    Task<IEnumerable<Individual>> FindAll();
    Task<Individual?> FindOne(int id);
    Task<Individual?> Create(Individual individual);
    Individual? Update(Individual individual);
    Individual? Delete(Individual individual);
}