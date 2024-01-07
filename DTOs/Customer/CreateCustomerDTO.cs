using System.ComponentModel.DataAnnotations;

namespace ExampleApi.DTOs;

public class CreateCustomerDTO
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = "";
    
    [Required]
    [MaxLength(14)]
    public string Document { get; set; } = "";
    
    [Required]
    [MaxLength(20)]
    public string Birth { get; set; } = "";
    
    [Required]
    [MaxLength(80)]
    public string Street { get; set; } = "";
    
    [Required]
    [MaxLength(100)]
    public string Number { get; set; } = "";
    
    [Required]
    [MaxLength(40)]
    public string Neighborhood { get; set; }    = "";
    
    [MaxLength(30)]
    public string Complement { get; set; } = "";
    
    [Required]
    [MaxLength(10)]
    public string Code { get; set; } = "";
    
    [Required]
    [MaxLength(80)]
    public string City { get; set; } = "";
    
    [Required]
    [MaxLength(2)]
    public string State { get; set; } = "";
    
    [Required]
    [MaxLength(14)]
    public string Phone { get; set; } = "";
    
    [Required]
    [MaxLength(15)]
    public string Cellphone { get; set; } = "";
    
    [Required]
    [MaxLength(100)]
    [EmailAddress]
    public string Email { get; set; } = "";
}