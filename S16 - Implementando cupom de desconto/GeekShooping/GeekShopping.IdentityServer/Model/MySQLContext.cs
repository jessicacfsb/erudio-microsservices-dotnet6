using GeekShopping.IdentityServer.Model.Context;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.IdentityServer.Model {
    public class MySQLContext : IdentityDbContext<ApplicationUser> {

        public MySQLContext(DbContextOptions<MySQLContext> options) 
            : base(options) { }

    }
}
