using System;
using Core.Entities;

namespace Core.Interfaces;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetProductsAsync();

    Task<Product?> GetProductByIdAsync(int id);

    void Add(Product product);

    void Delete(Product product);

    void Update(Product product);

    bool ProductExists(int id);

    Task<bool> SaveChangesAsync(); 
}
