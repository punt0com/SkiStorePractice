
using Core.Entities;

namespace Core.Interfaces;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetAll(string? brand, string? type,string? sort);

    Task<Product?> GetById(int id);

    Task<IReadOnlyList<string>> GetBrandsAsync();

    Task<IReadOnlyList<string>> GetTypeAsync();

    void Create(Product product);

    void Update(Product product);

    void Delete(Product product);

    bool Exist(int id);

    Task<bool> SaveChangesAsync();

}
