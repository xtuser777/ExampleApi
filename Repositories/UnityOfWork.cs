
using ExampleApi.Contexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace ExampleApi.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private ExampleApiContext _context;
    private IAddressRepository _addressRepository;
    private IContactRepository _contactRepository;
    private IIndividualRepository _individualRepository;
    private ICustomerRepository _customerRepository;
    private IUserRepository _userRepository;

    public UnitOfWork(ExampleApiContext context)
    {
        _context = context;
    }

    public ExampleApiContext Context { get { return _context; } }

    public IAddressRepository AddressRepository
    {
        get { return _addressRepository ?? new AddressRepository(_context); }
    }

    public IContactRepository ContactRepository
    {
        get { return _contactRepository ?? new ContactRepository(_context); }
    }

    public IIndividualRepository IndividualRepository
    {
        get { return _individualRepository ?? new IndividualRepository(_context); }
    }

    public ICustomerRepository CustomerRepository
    {
        get { return _customerRepository ?? new CustomerRepository(_context); }
    }

    public IUserRepository UserRepository
    {
        get { return _userRepository ?? new UserRepository(_context); }
    }

    public IDbContextTransaction BeginTransaction => _context.Database.BeginTransaction();

    public async Task Commit(IDbContextTransaction transaction)
    {
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}