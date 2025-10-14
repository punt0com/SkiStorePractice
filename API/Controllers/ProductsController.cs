
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{

    private readonly StoreContext context;

    public ProductsController(StoreContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return await context.Products.ToListAsync();
    }

    [HttpGet("{id:int}")] // api/Products/2
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await context.Products.FindAsync(id);

        if (product == null) return NotFound();

        return product;
    }


    // public async Task<ActionResult<Product>> CreateProduct([FromBody]Product product)
    //Gracias al atributo de APiController es no tenemos que especificar de donde viene el product 
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {

        context.Products.Add(product);

        await context.SaveChangesAsync();

        return product;


    }


    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id, Product product)
    {
        if (product.Id != id || !ProductExist(id)) return BadRequest("Cannot Update this product");

        context.Entry(product).State = EntityState.Modified;

        await context.SaveChangesAsync();

        return NoContent();
    }


    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        if (!ProductExist(id)) return BadRequest("Product not found delete this product");

        var product = await context.Products.FindAsync(id);

        context.Products.Remove(product!);

        await context.SaveChangesAsync();


        return NoContent();
    }


    private bool ProductExist(int id)
    {
        return context.Products.Any(x => x.Id == id);
    }

}
