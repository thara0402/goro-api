using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.Logging;

namespace Goro.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(config =>
                {
                    var settings = config.Build();

                    //config.AddAzureAppConfiguration(options =>
                    //    options.ConnectWithManagedIdentity(settings["AppConfig:Endpoint"]));

                    config.AddAzureKeyVault(settings["KeyVault:Endpoint"]);
                })
                .UseApplicationInsights()
                .UseStartup<Startup>();
    }
}
