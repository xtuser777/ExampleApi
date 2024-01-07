using ExampleApi.DTOs;
using ExampleApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExampleApi.Services;

public interface IUserService
{
    Task<IEnumerable<UserDTO>> Find();
    Task<UserDTO?> FindOne(int id);
    Task<UserDTO?> Create(CreateUserDTO entity);
    Task<UserDTO?> Update(int id, UpdateUserDTO entity);
    Task<UserDTO?> Delete(int id);
}