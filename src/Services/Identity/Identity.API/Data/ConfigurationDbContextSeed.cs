using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.API.Configuration;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.Extensions.Configuration;

namespace Identity.API.Data
{
    public static class ConfigurationDbContextSeed
    {
        public static async Task SeedAsync(ConfigurationDbContext context, IConfiguration configuration)
        {
            if (!context.Clients.Any())
            {
                var clientUrls = new Dictionary<string, string>
                {
                    ["AspNetRunBasics"] = configuration.GetValue<string>("AspNetRunBasicsClient"),
                };

                foreach (var client in Config.GetClients(clientUrls))
                {
                    context.Clients.Add(client.ToEntity());
                }

                await context.SaveChangesAsync();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var identity in Config.GetIdentityResources())
                {
                    context.IdentityResources.Add(identity.ToEntity());
                }

                await context.SaveChangesAsync();
            }

            if (!context.ApiResources.Any())
            {
                foreach (var api in Config.GetApiResources())
                {
                    context.ApiResources.Add(api.ToEntity());
                }

                await context.SaveChangesAsync();
            }

            if (!context.ApiScopes.Any())
            {
                foreach (var scope in Config.GetApiScopes())
                {
                    context.ApiScopes.Add(scope.ToEntity());
                }

                await context.SaveChangesAsync();
            }
        }
    }
}