using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using infotv.Models;
using Microsoft.Extensions.Logging;

namespace infotv
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string dbconn = Environment.GetEnvironmentVariable("DBCONN");
            string rediscon = Environment.GetEnvironmentVariable("REDISCONN");

            services.AddMvc();
            services.AddDbContext<InfoTVContext>(
                options => options.UseMySql(dbconn)
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging")); //log lev
            loggerFactory.AddDebug(); //does all log levels

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UsePathBase("/api");
            app.UseMvc(
                routes => {
                    routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
