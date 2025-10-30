using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductRepository repo) : ControllerBase
{

    // private readonly StoreContext context;

    // public ProductsController(StoreContext context)
    // {
    //     this.context = context;
    // }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string? brand, string? type, [FromQuery] string? sort)
    {
        // return await context.Products.ToListAsync();

        return Ok(await repo.GetAll(brand, type, sort));
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IEnumerable<Product>>> GetBrands()
    {
        // return await context.Products.ToListAsync();

        return Ok(await repo.GetBrandsAsync());
    }

    [HttpGet("types")]
    public async Task<ActionResult<IEnumerable<Product>>> GetTypes()
    {
        // return await context.Products.ToListAsync();

        return Ok(await repo.GetTypeAsync());
    }

    [HttpGet("{id:int}")] // api/Products/2
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        // var product = await context.Products.FindAsync(id);

        var product = await repo.GetById(id);

        if (product == null) return NotFound();

        return product;
    }


    // public async Task<ActionResult<Product>> CreateProduct([FromBody]Product product)
    //Gracias al atributo de APiController es no tenemos que especificar de donde viene el product 
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {

        repo.Create(product);

        if (await repo.SaveChangesAsync())
        {
            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // context.Products.Add(product);

        // await context.SaveChangesAsync();

        // return product;

        return BadRequest("There was a problem creating a product");

    }


    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id, Product product)
    {
        if (product.Id != id || !ProductExist(id)) return BadRequest("Cannot Update this product");


        repo.Update(product);

        if (await repo.SaveChangesAsync())
        {
            return NoContent();
        }

        // context.Entry(product).State = EntityState.Modified;

        // await context.SaveChangesAsync();

        return BadRequest("Cannot Update");
    }


    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        if (!ProductExist(id)) return BadRequest("Product not found cannot delete this product");

        var product = await repo.GetById(id);

        repo.Delete(product!);

        if (await repo.SaveChangesAsync())
        {
            return NoContent();
        }


        return BadRequest("Cannot Update");
    }


    private bool ProductExist(int id)
    {
        // return context.Products.Any(x => x.Id == id);

        return repo.Exist(id);
    }

}
