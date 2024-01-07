using ExampleApi.DTOs;
using ExampleApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExampleApi.Services;

public interface ICustomerService
{
    Task<IEnumerable<CustomerDTO>> Find();
    Task<CustomerDTO?> FindOne(int id);
    Task<CustomerDTO?> Create(CreateCustomerDTO entity);
    Task<CustomerDTO?> Update(int id, UpdateCustomerDTO entity);
    Task<CustomerDTO?> Delete(int id);
}