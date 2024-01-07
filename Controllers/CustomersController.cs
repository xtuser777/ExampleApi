using ExampleApi.DTOs;
using ExampleApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExampleApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class CustomersController : ControllerBase
{
    private ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    [Produces("application/json")]
    public async Task<ActionResult<IEnumerable<CustomerDTO>>> Index()
    {
        return Ok(await _customerService.Find());
    }

    [HttpGet("{id:int}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CustomerDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerDTO?>> Show(int id)
    {
        try
        {
            if (id <= 0) return BadRequest("Id inválido.");
            var customer = await _customerService.FindOne(id);
            if (customer is null) return NotFound();
            return Ok(customer);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CustomerDTO?>> Create(CreateCustomerDTO dto)
    {
        try
        {
            var entity = await _customerService.Create(dto);
            return Created("", entity);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id:int}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CustomerDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerDTO?>> Update(int id, UpdateCustomerDTO dto)
    {
        try
        {
            if (id <= 0 || id != dto.Id) return BadRequest("Id inválido");
            var entity = await _customerService.Update(id, dto);
            return Ok(entity);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CustomerDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerDTO?>> Delete(int id)
    {
        try
        {
            if (id <= 0) return BadRequest("Id inválido.");
            var entity = await _customerService.Delete(id);
            return Ok(entity);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}