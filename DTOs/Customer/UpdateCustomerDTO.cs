using System.ComponentModel.DataAnnotations;

namespace ExampleApi.DTOs;

public class UpdateCustomerDTO : CreateCustomerDTO
{
    [Required]
    public int Id { get; set; } = 0;
}