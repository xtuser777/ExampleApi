using ExampleApi.DTOs;
using ExampleApi.Models;

namespace ExampleApi.Services;

public interface ITokenService
{
    string GerarToken(string key, string issuer,string audience, LoginDTO user);
}
