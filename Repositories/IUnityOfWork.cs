using ExampleApi.Contexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace ExampleApi.Repositories;

public interface IUnitOfWork
{
    ExampleApiContext Context { get; }
    IDbContextTransaction BeginTransaction { get; }
    IAddressRepository AddressRepository { get; }
    IContactRepository ContactRepository { get; }
    IIndividualRepository IndividualRepository { get; }
    ICustomerRepository CustomerRepository { get; }
    IUserRepository UserRepository { get; }
    Task Commit(IDbContextTransaction transaction);
    void Dispose();
}