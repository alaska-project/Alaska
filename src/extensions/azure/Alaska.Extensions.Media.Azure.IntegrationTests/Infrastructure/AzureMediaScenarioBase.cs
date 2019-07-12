using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Alaska.Extensions.Media.Azure.IntegrationTests.Infrastructure
{
    public abstract class AzureMediaScenarioBase
    {
        protected const string MediaLibraryApi = "/alaska/api/media";

        protected TestServer CreateServer()
        {
            var path = Assembly.GetAssembly(typeof(AzureMediaServerStartup))
                .Location;

            var hostBuilder = new WebHostBuilder()
                .UseContentRoot(Path.GetDirectoryName(path))
                .ConfigureAppConfiguration(cb =>
                {
                    cb.AddJsonFile("appsettings.json", optional: false)
                    .AddEnvironmentVariables();
                })
                    .UseStartup<AzureMediaServerStartup>();

            var testServer = new TestServer(hostBuilder);
            return testServer;
        }

        protected T GetService<T>(TestServer server)
        {
            return server.Host.Services.GetRequiredService<T>();
        }
    }
}
