namespace ExampleApi.DTOs;

public class AuthDTO
{
    public string? UserName { get; set; }
    public string? Token { get; set; }
    public DateTime Expiration { get; set; }
}