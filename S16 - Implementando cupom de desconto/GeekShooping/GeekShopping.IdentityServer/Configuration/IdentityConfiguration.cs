using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace GeekShopping.IdentityServer.Configuration {
    public static class IdentityConfiguration {
        public const string Admin = "Admin";
        public const string Client = "Client";

        //nome de grupos de claims que podem ser requisitados chamando um parametro de escopo
        //São recursos a serem protegidos pelo identity server ou pela api (dados do usuario, id do usuario, nome, email)
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource> {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
            };
        //definindo o primeiro api scope (sao identificadores ou recursos que o client pode acessar)
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope> {                   //display name
                new ApiScope("geek_shopping", "GeekShopping Server"),
                new ApiScope(name: "read", "Read data."),
                new ApiScope(name: "write", "Write data."),
                new ApiScope(name: "delete", "Delete data.")
            };

        //Criando o client (Requisita um token ao identity server), ex. a aplicação
        public static IEnumerable<Client> Clients =>
            new List<Client> {

                new Client {
                    ClientId = "client",
                    ClientSecrets = {new Secret("my_super_secret".Sha256()) }, //usar uma secret complexa pq pode comprometer a segurança, isto é usado para encriptar e decriptar o token
                    //adicionando as credenciais (GrantTypes)
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = {"read", "write", "profile"}
                },

                new Client {
                    ClientId = "geek_shopping",
                    ClientSecrets = {new Secret("my_super_secret".Sha256()) }, //usar uma secret complexa pq pode comprometer a segurança, isto é usado para encriptar e decriptar o token
                    //adicionando as credenciais (GrantTypes)
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = {"https://localhost:4430/signin-oidc" }, //geekshopping web
                    PostLogoutRedirectUris = {"https://localhost:4430/signout-callback-oidc" },
                    AllowedScopes = new List<string> {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "geek_shopping"
                    }
                }
            };

    }
}
