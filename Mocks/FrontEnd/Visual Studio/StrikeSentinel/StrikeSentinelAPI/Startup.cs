using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using StrikeSentinelAPI.Models;

namespace StrikeSentinelAPI
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
            services.AddMvc();

            services.AddDbContext<StrikeNewsContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("StrikeNewsContext")));
            //usa base de dados em memória
            //services.AddDbContext<StrikeNewsContext>(options =>
            //          options.UseInMemoryDatabase("StrikeNews"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //cria base de dados caso a mesma não exista
                using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetRequiredService<StrikeNewsContext>();
                    context.Database.EnsureCreated();
                }

                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
