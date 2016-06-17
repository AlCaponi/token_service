using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TokenService.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging;

namespace aspnetcoreapp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ITokenRepository, MemoryTokenRepository>();
            // Add framework services.
            services.AddMvc();
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();
            app.UseMvc();
        }
    }
}