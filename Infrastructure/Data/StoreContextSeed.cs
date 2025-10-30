using System.Text.Json;
using Core.Entities;

namespace Infrastructure.Data;

public class StoreContextSeed
{

    public static async Task SeedAsync(StoreContext context)
    {
        if (!context.Products.Any())
        {
            var productsData = await File.ReadAllTextAsync("../Infrastructure/Data/Seeds/products.json");

            var produtcs = JsonSerializer.Deserialize<List<Product>>(productsData);

            if (produtcs == null) return;

            context.Products.AddRange(produtcs);

            await context.SaveChangesAsync();
        }
    }
}
