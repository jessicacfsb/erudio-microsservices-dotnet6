using Geekshopping.CartAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CartAPI.Model.Context {

    public class MySQLContext : DbContext {
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { }
        
        //Adicionar o DbSet de Produtos ao Mapeamento
        public DbSet<Product> Products { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<CartHeader> CartHeaders { get; set; }
        
    }
}
