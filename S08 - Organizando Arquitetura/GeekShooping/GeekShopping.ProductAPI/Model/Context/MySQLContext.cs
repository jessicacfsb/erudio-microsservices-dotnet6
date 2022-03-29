using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Model.Context {

    public class MySQLContext : DbContext {
        //2contrutores
        public MySQLContext() { }
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { }
        
        //Adicionar o DbSet de Produtos ao Mapeamento
        public DbSet<Product> Products { get; set; }
    }
}
