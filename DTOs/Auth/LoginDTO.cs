using System.ComponentModel.DataAnnotations;

namespace ExampleApi.DTOs;

public class LoginDTO
{
    [Required]
    [MaxLength(20)]
    public string UserName { get; set; } = "";
    
    [Required]
    [MinLength(6)]
    [MaxLength(10)]
    public string Password { get; set; } = "";
}