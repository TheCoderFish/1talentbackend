using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace OneTalentResignation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseUrls("http://192.168.0.76:8030")
            .UseStartup<Startup>();
    }
}
