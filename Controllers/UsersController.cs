using ExampleApi.DTOs;
using ExampleApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExampleApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UsersController : ControllerBase
{
    private IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Produces("application/json")]
    public async Task<ActionResult<IEnumerable<UserDTO>>> Index()
    {
        try
        {
            var users = await _userService.Find();
            return Ok(users);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id:int}")]
    [Produces("application/json")]
    public async Task<ActionResult<UserDTO?>> Show(int id)
    {
        try
        {
            var user = await _userService.FindOne(id);
            if (user is null) return NotFound();
            return Ok(user);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Produces("application/json")]
    public async Task<ActionResult<UserDTO?>> Create(CreateUserDTO dto)
    {
        try
        {
            var entity = await _userService.Create(dto);
            return Created("", entity);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id:int}")]
    [Produces("application/json")]
    public async Task<ActionResult<UserDTO?>> Update(int id, UpdateUserDTO dto)
    {
        try
        {
            if (id != dto.Id) return BadRequest("Id inválido.");
            var entity = await _userService.Update(id, dto);
            return Ok(entity);
        }
        catch (Exception e)
        {
            return e.Message.StartsWith("404") ? NotFound() : BadRequest(e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    [Produces("application/json")]
    public async Task<ActionResult<UserDTO?>> Delete(int id)
    {
        try
        {
            if (id <= 0) return BadRequest("Id inválido.");
            var entity = await _userService.Delete(id);
            return Ok(entity);
        }
        catch (Exception e)
        {
            return e.Message.StartsWith("404") ? NotFound() : BadRequest(e.Message);
        }
    }
}