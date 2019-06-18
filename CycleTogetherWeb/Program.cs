using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace CycleTogetherWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();

        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((webHostBuilderContext,
                                        configurationbuilder) =>
            {
                var env = webHostBuilderContext.HostingEnvironment;
                configurationbuilder.SetBasePath(env.ContentRootPath);
                configurationbuilder.AddJsonFile("appsettings.json", false, true);
                configurationbuilder.AddJsonFile("emailconfig.json", false, true);
                configurationbuilder.AddJsonFile("cloudinaryCredentials.json", false, true);
                //TODO: Fix connection string from separate config file.
                //configurationbuilder.AddJsonFile("connectionStrings.json", false, true);
                configurationbuilder.AddEnvironmentVariables();
            })
                .UseStartup<Startup>();


    }
}
