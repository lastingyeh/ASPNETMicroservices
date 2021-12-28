using System;
using System.Net.Http;
using AspnetRunBasics.Extensions;
using AspnetRunBasics.Services;
using Common.Logging;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Polly;
using Polly.Extensions.Http;
using Serilog;

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

            services.AddHttpContextAccessor();

            services.AddTransient<AuthenticationDelegatingHandler>();
            services.AddTransient<LoggingDelegatingHandler>();

            var apiUri = new Uri(Configuration["ApiSettings:GatewayAddress"]);

            services.AddHttpClient<ICatalogService, CatalogService>(c => c.BaseAddress = apiUri)
                .AddHttpMessageHandler<LoggingDelegatingHandler>()
                .AddPolicyHandler(GetRetryPolicy())
                .AddPolicyHandler(GetCircuitBreakingPolicy());

            services.AddHttpClient<IBasketService, BasketService>(c =>
            {
                c.BaseAddress = apiUri;
                c.DefaultRequestHeaders.Clear();
                c.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            })
                .AddHttpMessageHandler<AuthenticationDelegatingHandler>()
                .AddHttpMessageHandler<LoggingDelegatingHandler>()
                .AddPolicyHandler(GetRetryPolicy())
                .AddPolicyHandler(GetCircuitBreakingPolicy());

            services.AddHttpClient<IOrderService, OrderService>(c => c.BaseAddress = apiUri)
                .AddHttpMessageHandler<LoggingDelegatingHandler>()
                .AddPolicyHandler(GetRetryPolicy())
                .AddPolicyHandler(GetCircuitBreakingPolicy());

            services.AddCustomAuthentication(Configuration);

            services.AddHealthChecks()
                .AddUrlGroup(apiUri, "Ocelot API Gw", HealthStatus.Degraded);
        }


        // In this case will wait for
        // 2 ^ 1 secs then
        // 2 ^ 2 secs then
        // 2 ^ 3 secs then
        // 2 ^ 4 secs then
        // 2 ^ 5 secs then
        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy() =>
            HttpPolicyExtensions.HandleTransientHttpError()
                .WaitAndRetryAsync(
                    retryCount: 5,
                    sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    onRetry: (ex, retryCount, context) =>
                    {
                        Log.Error($"Retry {retryCount} of {context.PolicyKey} at {context.OperationKey}, due to : {ex}.");
                    });

        private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakingPolicy() =>
            HttpPolicyExtensions.HandleTransientHttpError()
                .CircuitBreakerAsync(handledEventsAllowedBeforeBreaking: 5, durationOfBreak: TimeSpan.FromSeconds(30));

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

            // app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.Lax });

            app.UseRouting();

            app.UseAuthentication();
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
