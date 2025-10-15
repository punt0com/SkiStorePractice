using System;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ProductsRepository(StoreContext context) : IProductRepository
{
    // private readonly StoreContext context;

    // public ProductsRepository(StoreContext context)
    // {
    //     this.context = context;
    // }
    public void Add(Product product)
    {
        context.Products.Add(product);
        
    }

    public void Delete(Product product)
    {
        context.Products.Remove(product);
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await context.Products.FindAsync(id);
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        return await context.Products.ToListAsync();
    }

    public bool ProductExists(int id)
    {
        return context.Products.Any(x => x.Id == id);
    }

    public  async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public void Update(Product product)
    {
        context.Entry(product).State = EntityState.Modified;
    }
}
