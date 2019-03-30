using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WebapiTutorial.Models;

namespace WebapiTutorial.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class ProductsController : ControllerBase {
    private ProductContext _context;
  
    public ProductsController(ProductContext context) {
      _context = context;

      if (_context.Products.Count() == 0) {
        _context.Products.Add(new Product {
          Name= "iPhone 5c",
          Price= 100
        });
        _context.SaveChanges();
      }
    }

    // GET: api/products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts() { 
      return await this._context.Products.ToListAsync();
    }

    // GET: api/products/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(string id)
    {
      Product foundProduct = await this._context.Products.FindAsync(new Guid(id));
      if (foundProduct == null) {
        return NotFound();
      }

      return foundProduct;
    }

    // POST: api/products
    [HttpPost]
    public async Task<ActionResult<Product>> PostNewProduct(Product newProduct)
    {
      _context.Products.Add(newProduct);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetProduct), new { id = newProduct.Id }, newProduct );
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Product>> PutProduct(Guid id, Product product)
    {
      if (id.CompareTo(product.Id) != 0) {
        return BadRequest();
      }

      _context.Entry(product).State = EntityState.Modified;
      await this._context.SaveChangesAsync();

      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Product>> DeleteProduct(Guid id) {
      Product foundProduct = await this._context.Products.FindAsync(id);
      if (foundProduct == null) {
        return NotFound();
      }

      this._context.Products.Remove(foundProduct);
      await this._context.SaveChangesAsync();

      return NoContent();
    }
  }
}