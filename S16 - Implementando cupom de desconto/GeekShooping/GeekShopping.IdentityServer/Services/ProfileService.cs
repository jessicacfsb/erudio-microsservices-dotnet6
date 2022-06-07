using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using GeekShopping.IdentityServer.Model.Context;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace GeekShopping.IdentityServer.Services {
    public class ProfileService : IProfileService {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;

        public ProfileService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory) {
            _userManager = userManager;
            _roleManager = roleManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context) {
            string id = context.Subject.GetSubjectId();
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            ClaimsPrincipal userClaims = await _userClaimsPrincipalFactory.CreateAsync(user);
            //converter as claims para lista
            List<Claim> claims = userClaims.Claims.ToList();
            //passar o primeiro e ultimo nome do usuário
            claims.Add(new Claim(JwtClaimTypes.FamilyName, user.LastName));
            claims.Add(new Claim(JwtClaimTypes.GivenName, user.FirstName));

            //se o user name suportar use roles, adicionar user roles
            if (_userManager.SupportsUserRole) {
                IList<string> roles = await _userManager.GetRolesAsync(user);
                //iterar sobre todos os roles
                foreach (string role in roles) {
                    claims.Add(new Claim(JwtClaimTypes.Role, role));
                    //verificar se o role manager suporta o role clients
                    if (_roleManager.SupportsRoleClaims) {
                        IdentityRole identityRole = await _roleManager.FindByNameAsync(role);
                        if (identityRole != null)
                            claims.AddRange(await _roleManager.GetClaimsAsync(identityRole));
                    }
                }
            }
            //adicionar as claims ao contexto
            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context) {
            string id = context.Subject.GetSubjectId();
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            context.IsActive = user != null;
        }
    }
}
