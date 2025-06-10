using System;
using System.Text.Json;
using Core.Entities;

namespace Infrastructure.DataAccess.SeedData;

public class StoreContextSeedData
{
    public static async Task SeedAsync(StoreContext context)

    {
        if (!context.Products.Any())
        {
            var productseedData = await File.ReadAllTextAsync("../Infrastructure/DataAccess/SeedData/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(productseedData);

            if (products == null) return;

            context.Products.AddRange(products);

            await context.SaveChangesAsync();               
        }
        }
}
