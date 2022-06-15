using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CuponAPI.Model.Context
{

    public class MySQLContext : DbContext {
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { }
        
        //Adicionar o DbSet de Produtos ao Mapeamento
        public DbSet<Cupon> Cupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Cupon>().HasData(new Cupon
            {
                Id = 1,
                CuponCode = "ERUDIO_2022_10",
                DiscountAmount = 10
            });
            modelBuilder.Entity<Cupon>().HasData(new Cupon
            {
                Id = 2,
                CuponCode = "ERUDIO_2022_15",
                DiscountAmount = 15
            });
        }

    }
}
