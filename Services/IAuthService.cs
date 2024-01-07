using ExampleApi.DTOs;

namespace ExampleApi.Services;

public interface IAuthService
{
    Task<AuthDTO> Login(LoginDTO dto);
}