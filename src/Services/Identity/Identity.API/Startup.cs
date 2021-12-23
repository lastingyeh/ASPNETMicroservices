using System;
using System.Reflection;
using HealthChecks.UI.Client;
using Identity.API.Certificates;
using Identity.API.Data;
using Identity.API.Devspaces;
using Identity.API.Models;
using Identity.API.Services;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Identity.API
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
            var connectionString = Configuration.GetConnectionString("IdentityConnectionString");
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<ApplicationDbContext>(opts =>
            {
                opts.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOpts =>
                {
                    sqlOpts.MigrationsAssembly(migrationsAssembly);
                    sqlOpts.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                });
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<ILoginService<ApplicationUser>, EFLoginService>();
            services.AddTransient<IRedirectService, RedirectService>();

            // Add IdentityServer
            services.AddIdentityServer(x =>
            {
                x.IssuerUri = "null";
                x.Authentication.CookieLifetime = TimeSpan.FromHours(2);
            })
            .AddDevspacesIfNeeded(Configuration.GetValue("EnableDevspaces", false))
            .AddSigningCredential(Certificate.Get())
            .AddAspNetIdentity<ApplicationUser>()
            .AddConfigurationStore(opts =>
            {
                opts.ConfigureDbContext = builder => builder.UseSqlServer(connectionString,
                    sqlServerOptionsAction: sqlOpts =>
                    {
                        sqlOpts.MigrationsAssembly(migrationsAssembly);
                        sqlOpts.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    });
            })
            .AddOperationalStore(opts =>
            {
                opts.ConfigureDbContext = builder => builder.UseSqlServer(connectionString,
                    sqlServerOptionsAction: sqlOpts =>
                    {
                        sqlOpts.MigrationsAssembly(migrationsAssembly);
                        sqlOpts.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    });
            })
            .Services.AddTransient<IProfileService, ProfileService>();

            services.AddControllers();

            services.AddControllersWithViews();

            services.AddRazorPages();

            services.AddHealthChecks()
                    .AddDbContextCheck<ApplicationDbContext>();

            services.AddHealthChecks()
                    .AddDbContextCheck<ConfigurationDbContext>();

            services.AddHealthChecks()
                    .AddDbContextCheck<PersistedGrantDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                // app.UseHsts();
            }

            var pathBase = Configuration["PATH_BASE"];

            if (!string.IsNullOrEmpty(pathBase))
            {
                loggerFactory.CreateLogger<Startup>().LogDebug("Using PATH BASE '{pathBase}'", pathBase);

                app.UsePathBase(pathBase);
            }

            // app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("Content-Security-Policy", "script-src 'unsafe-inline'");

                await next();
            });

            app.UseForwardedHeaders();

            app.UseIdentityServer();

            app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();

                endpoints.MapControllers();

                endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

                endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
                {
                    Predicate = r => r.Name.Contains("self")
                });
            });
        }
    }
}
