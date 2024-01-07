using System.ComponentModel.DataAnnotations;

namespace ExampleApi.DTOs;

public class UpdateUserDTO : CreateUserDTO
{
    [Required]
    public int Id { get; set; } = 0;
}