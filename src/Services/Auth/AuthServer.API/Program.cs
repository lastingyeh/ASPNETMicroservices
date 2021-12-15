using AuthServer.API.Data;
using AuthServer.API.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace AuthServer.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            host.MigrateDatabase<AuthContext>((context, services) =>
            {
                // var logger = services.GetService<ILogger<OrderContextSeed>>();

                // OrderContextSeed.SeedAsync(context, logger).Wait();
            })
            .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
