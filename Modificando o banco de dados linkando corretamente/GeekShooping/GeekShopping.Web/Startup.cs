using GeekShopping.Web.Services;
using GeekShopping.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekShopping.Web {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddHttpClient<IProductService, ProductService>(c =>
                    c.BaseAddress = new Uri(Configuration["ServiceUrls:ProductAPI"])
                );
            services.AddControllersWithViews();
            services.AddAuthentication(options => {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
                .AddCookie("Cookies", c => c.ExpireTimeSpan = TimeSpan.FromMinutes(10))
                .AddOpenIdConnect("oidc", options => {
                    options.Authority = Configuration["ServiceUrls:IdentityServer"]; //pega a url do app settings
                    options.GetClaimsFromUserInfoEndpoint = true;  //obter as claims do usuario
                    options.ClientId = "geek_shopping";            //do identity configuration
                    options.ClientSecret = "my_super_secret";      //qual a secret que o client setou como secret
                    options.ResponseType = "code";
                    options.ClaimActions.MapJsonKey("role", "role", "role"); //mapear as claims pro role do usuario
                    options.ClaimActions.MapJsonKey("sub", "sub", "sub"); //mapear as claims pro sub do usuario
                    options.TokenValidationParameters.NameClaimType = "name";  //adicionar a options de validação dos parametros
                    options.TokenValidationParameters.RoleClaimType = "role";
                    options.Scope.Add("geek_shopping");  //adicionar o escoopo
                    options.SaveTokens = true;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();//tem que estar antes do Authorizations pq se nao não funciona

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}