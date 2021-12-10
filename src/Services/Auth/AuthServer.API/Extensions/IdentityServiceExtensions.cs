using System;
using System.Text;
using AuthServer.API.Configurations;
using AuthServer.API.Data;
using AuthServer.API.Entities;
using AuthServer.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace AuthServer.API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            // otions authentication configuration
            services.Configure<AuthConfiguration>(config.GetSection("Authentication"));
            
            // token services added
            services.AddScoped<TokenService>();

            // identity store added & userManager & signInManager
            services.AddIdentityCore<AppUser>(opts =>
            {
                opts.Password.RequireNonAlphanumeric = false;
                // opts.SignIn.RequireConfirmedAccount = true;
            })
            .AddEntityFrameworkStores<AuthContext>()
            .AddSignInManager<SignInManager<AppUser>>()
            .AddDefaultTokenProviders();

            // Authorize user for refresh token used
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                var authConfig = new AuthConfiguration();
                // var keyVaultClient = new SecretClient
                // (
                //     new Uri(_config.GetValue<string>("KeyVaultUri")),
                //     new DefaultAzureCredential()
                // );
                // authConfig.AccessTokenSecret = keyVaultClient.GetSecret("access-token-secret").Value.Value;

                config.GetSection("Authentication").Bind(authConfig);

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfig.AccessTokenSecret)),
                    ValidIssuer = authConfig.Issuer,
                    ValidAudience = authConfig.Audience,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ClockSkew = TimeSpan.Zero,
                };
            });

            return services;
        }
    }
}