using System;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ProductsRepository(StoreContext context) : IProductRepository
{
    public void Create(Product product)
    {
        context.Products.Add(product);
    }

    public void Delete(Product product)
    {
        context.Products.Remove(product);
    }


    public async Task<Product?> GetById(int id)
    {
        return await context.Products.FindAsync(id);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public void Update(Product product)
    {
        context.Entry(product).State = EntityState.Modified;
    }

    public bool Exist(int id)
    {
        return context.Products.Any(x => x.Id == id);
    }

    public async Task<IReadOnlyList<Product>> GetAll(string? brand, string? type, string? sort)
    {

        var query = context.Products.AsQueryable();

        if (!string.IsNullOrEmpty(brand))
        {
            query = query.Where(x => x.Brand == brand);
        }

        if (!string.IsNullOrEmpty(type))
        {
            query = query.Where(x => x.Brand == type);
        }

        query = sort switch
        {
            "priceAsc" => query.OrderBy(x => x.Price),
            "priceDesc" => query.OrderByDescending(x => x.Price),
            _ => query.OrderBy(x => x.Name)
        };

        return await query.ToListAsync();
    }

    public async Task<IReadOnlyList<string>> GetBrandsAsync()
    {
        return await context.Products.Select(s => s.Brand).Distinct().ToListAsync();
    }

    public async Task<IReadOnlyList<string>> GetTypeAsync()
    {
        return await context.Products.Select(s => s.Type).Distinct().ToListAsync();
    }

}
