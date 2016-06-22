using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TokenService.Repositories;
using Microsoft.Extensions.Logging;

namespace TokenService.Hosting
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ITokenRepository, MemoryTokenRepository>();
            // Add framework services.
            services.AddLeanMvc();
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if(env.IsDevelopment())
            {
                loggerFactory.AddConsole();
                loggerFactory.AddDebug();
            }

            if(env.IsProduction() || env.IsStaging())
            {
                // Enable Metrics
            }

            app.UseMvc();
        }


    }
}