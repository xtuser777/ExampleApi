using System.Linq.Expressions;
using ExampleApi.Models;

namespace ExampleApi.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> FindAll();
    Task<User?> FindOne(Expression<Func<User?, bool>> predicate);
    Task<User?> Create(User user);
    User? Update(User user);
    User? Delete(User user);
}