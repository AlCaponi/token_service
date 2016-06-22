using Microsoft.AspNetCore.Hosting;

namespace TokenService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Hosting.Startup>()
                .Build();

            host.Run();
        }
    }
}