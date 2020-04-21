using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace VirusScanTest.Console
{
    public interface IAppSettings
    {
        string CloudmersiveApiKey { get; set; }
    }
    public class AppSettings : IAppSettings
    {
        public string CloudmersiveApiKey { get; set; }

        public static IAppSettings ReadFromJsonFile(string path = "appsettings.json")
        {
            IConfigurationRoot Configuration;

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path);

            Configuration = builder.Build();
            var settings = Configuration.Get<AppSettings>();

            if (string.IsNullOrWhiteSpace(settings.CloudmersiveApiKey))
            {
                throw new Exception("Cloudmersive Api Key is missing");
            }

            return Configuration.Get<AppSettings>();
        }
    }
}
