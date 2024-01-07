namespace ExampleApi.Models;

public class Customer
{
    public int Id { get; set; } = 0;
    public int IndividualId { get; set; } = 0;
    public Individual Individual { get; set; } = new Individual();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}