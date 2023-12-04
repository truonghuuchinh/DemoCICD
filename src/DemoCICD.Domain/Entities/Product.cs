using DemoCICD.Domain.Abstractions.Entities;

namespace DemoCICD.Domain.Entities;
public class Product : DomainEntity<Guid>
{

    public Product(string name, decimal price, string description)
    {
        Name = name;
        Price = price;
        Description = description;
    }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public string Description { get; set; }

    public bool? IsDelete { get; set; } = false;
}
