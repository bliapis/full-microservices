using AspnetRunBasics.HttpHandlers;
using AspnetRunBasics.Services;
using HealthChecks.UI.Client;
using IdentityModel.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using System;

namespace AspnetRunBasics
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            //TODO: Create a bootstrap layer to register that kind of services
            services.AddHttpClient<ICatalogService, CatalogService>(c =>
                c.BaseAddress = new Uri(Configuration["ApiSettings:GatewayAddress"]));

            services.AddHttpClient<IBasketService, BasketService>(c =>
                c.BaseAddress = new Uri(Configuration["ApiSettings:GatewayAddress"]))
                .AddHttpMessageHandler<AuthenticationDelegatingHandler>();

            services.AddHttpClient<IOrderService, OrderService>(c =>
                c.BaseAddress = new Uri(Configuration["ApiSettings:GatewayAddress"]));

            services.AddHealthChecks()
                .AddUrlGroup(new Uri(Configuration["ApiSettings:GatewayAddress"]), "Ocelot API Gw", HealthStatus.Degraded);

            services.AddTransient<AuthenticationDelegatingHandler>();

            services.AddHttpClient("AuthClient", client =>
            {
                client.BaseAddress = new Uri(Configuration["AuthorizationSettings:AuthorizationUri"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            });

            services.AddSingleton(new ClientCredentialsTokenRequest
            {
                Address = $"{Configuration.GetValue<string>("AuthorizationSettings:AuthorizationUri")}/connect/token",

                ClientId = "ecommerceClient",
                ClientSecret = "secret",

                Scope = "basketAPI"
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });
        }
    }
}
