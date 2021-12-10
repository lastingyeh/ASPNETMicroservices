using System;
using System.Text;
using Basket.API.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Basket.API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            // Authorize user for refresh token used
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
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