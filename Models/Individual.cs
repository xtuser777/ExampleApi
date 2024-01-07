namespace ExampleApi.Models;

public class Individual
{
    public int Id { get; set; } = 0;
    public string Name { get; set; } = "";
    public string Document { get; set; } = "";
    public string Birth { get; set; } = "";
    public int AddressId { get; set; } = 0;
    public Address Address { get; set; } = new Address();
    public int ContactId { get; set; } = 0;
    public Contact Contact { get; set; } = new Contact();
}