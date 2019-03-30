using Microsoft.EntityFrameworkCore;

namespace WebapiTutorial.Models {
  public class ProductContext: DbContext {
    public ProductContext(DbContextOptions<ProductContext> options): base(options) {}

    public DbSet<Product> Products { set; get; } 
  }
}