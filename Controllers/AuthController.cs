using ExampleApi.DTOs;
using ExampleApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExampleApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [Produces("application/json")]
    public async Task<ActionResult<AuthDTO>> Login(LoginDTO dto)
    {
        try
        {
            if (dto is null) return BadRequest("Login inválido.");
            var result = await _authService.Login(dto);
            return Ok(result);
        }
        catch (Exception e)
        {
            return e.Message.Contains("401") ? Unauthorized() : BadRequest(e.Message);
        }
    }
}