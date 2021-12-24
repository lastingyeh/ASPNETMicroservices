using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspnetRunBasics.Extensions
{
    public static class CustomAuthenticationExtensions
    {
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var identityUrl = configuration.GetValue<string>("IdentityUrl");
            var callbackUrl = configuration.GetValue<string>("CallBackUrl");
            var clientId = configuration.GetValue<string>("ClientId");
            var clientSecret = configuration.GetValue<string>("ClientSecret");

            var sessionCookieLifetime = configuration.GetValue("SessionCookieLifetimeMinutes", 60);

            services.AddAuthentication(opts =>
            {
                opts.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(setup => setup.ExpireTimeSpan = TimeSpan.FromMinutes(sessionCookieLifetime))
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, opts =>
            {
                // opts.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                opts.Authority = identityUrl;

                opts.ClientId = clientId;
                opts.ClientSecret = clientSecret;

                opts.SignedOutRedirectUri = callbackUrl;
                // hybrid
                opts.ResponseType = "code id_token";
                opts.SaveTokens = true;
                opts.GetClaimsFromUserInfoEndpoint = true;
                opts.RequireHttpsMetadata = false;

                // scopes
                // opts.Scope.Add("openid");
                // opts.Scope.Add("profile");
                opts.Scope.Add("basket.checkout");
            });

            return services;
        }
    }
}