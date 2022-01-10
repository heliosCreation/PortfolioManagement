using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;
using Azure.Identity;
using System;

namespace Portfolio
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
               .ConfigureAppConfiguration((hostingContext, config) =>
               {
                   config.SetBasePath(Directory.GetCurrentDirectory());
                   config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                         .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
                   config.AddEnvironmentVariables();
                   config.AddCommandLine(args);
                   if (hostingContext.HostingEnvironment.IsProduction())
                   {
                       var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("VaultUri"));
                       config.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());
                   }
               });
    }
}
