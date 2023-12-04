using DemoCICD.Domain.Entities;

namespace DemoCICD.Persistence;
public static class SeedingData
{
    public static void Seeding(this ApplicationDbContext context)
    {
        if (!context.Products.Any())
        {
            context.AddRange(new List<Product>
            {
                new ("Product one", 10000, "This is product one to test"),
                new ("Product Two", 20000, "This is product two to test"),
                new ("Sam sung galaxy", 50000, "This is new product just import from us"),
            });
        }

        context.SaveChanges();
    }
}
