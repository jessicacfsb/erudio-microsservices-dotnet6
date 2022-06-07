using GeekShopping.IdentityServer.Configuration;
using GeekShopping.IdentityServer.Model;
using GeekShopping.IdentityServer.Model.Context;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace GeekShopping.IdentityServer.Initializer {
    public class DbInitializer : IDbInitializer {

        //3 atributos, eles sãao usados para manipular os obj no banco de dados
        private readonly MySQLContext _context;
        private readonly UserManager<ApplicationUser> _user;
        private readonly RoleManager<IdentityRole> _role;

        //construtor
        public DbInitializer(MySQLContext context,
            UserManager<ApplicationUser> user,
            RoleManager<IdentityRole> role) {
            _context = context;
            _user = user;
            _role = role;
        }

        //persistir no banco de dados
        //verificar se no banco já existe um perfil de admin
        public void Initialize() {
            if (_role.FindByNameAsync(IdentityConfiguration.Admin).Result != null) return;
            _role.CreateAsync(new IdentityRole(IdentityConfiguration.Admin)).GetAwaiter().GetResult();
            _role.CreateAsync(new IdentityRole(IdentityConfiguration.Client)).GetAwaiter().GetResult();

            ApplicationUser admin = new ApplicationUser() {
                UserName = "jessica-admin",
                Email = "jessica-admin@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "+55 (41) 12345-6789",
                FirstName = "Jessica",
                LastName = "Admin"
            };

            _user.CreateAsync(admin, "Jessica123$").GetAwaiter().GetResult();
            _user.AddToRoleAsync(admin, 
                IdentityConfiguration.Admin).GetAwaiter().GetResult();
            var adminClaims = _user.AddClaimsAsync(admin, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, $"{admin.FirstName} {admin.LastName}"),
                new Claim(JwtClaimTypes.GivenName, admin.FirstName),
                new Claim(JwtClaimTypes.FamilyName, admin.LastName),
                new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin),
            }).Result;

            ApplicationUser client = new ApplicationUser() {
                UserName = "jessica-client",
                Email = "jessica-client@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "+55 (41) 12345-6789",
                FirstName = "Jessica",
                LastName = "Client"
            };

            _user.CreateAsync(client, "Jessica123$").GetAwaiter().GetResult();
            _user.AddToRoleAsync(client, IdentityConfiguration.Client).GetAwaiter().GetResult();
            var clientClaims = _user.AddClaimsAsync(client, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, $"{client.FirstName} {client.LastName}"),
                new Claim(JwtClaimTypes.GivenName, client.FirstName),
                new Claim(JwtClaimTypes.FamilyName, client.LastName),
                new Claim(JwtClaimTypes.Role, IdentityConfiguration.Client),
            }).Result;
        }
    }
}
