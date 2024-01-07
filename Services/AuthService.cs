using ExampleApi.DTOs;
using ExampleApi.Repositories;
using ExampleApi.Utils;

namespace ExampleApi.Services;

public class AuthService : IAuthService
{
    private ITokenService _tokenService;
    private IUserService _userService;
    private IUnitOfWork _unitOfWork;
    private IConfiguration _configuration;

    public AuthService(ITokenService tokenService, IUserService userService, IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _tokenService = tokenService;
        _userService = userService;
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }

    public async Task<AuthDTO> Login(LoginDTO dto)
    {
        var user = await _unitOfWork.UserRepository.FindOne(u => u.UserName == dto.UserName);
        if (user is null) throw new Exception("401");
        if (!Crypt.VerifyHashedPassword(user.Password, dto.Password)) throw new Exception("401");
        var key = _configuration.GetValue<string>("Jwt:Key");
        var issuer = _configuration.GetValue<string>("Jwt:Issuer");
        var audience = _configuration.GetValue<string>("Jwt:Audience");
        var token = _tokenService.GerarToken(key, issuer, audience, dto);

        return new AuthDTO()
        {
            UserName = dto.UserName,
            Token = token,
            Expiration = DateTime.Now.AddDays(7),
        };
    }
}