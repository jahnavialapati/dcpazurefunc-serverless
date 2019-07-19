using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PromoServiceMongoDB.DataAccess;
using PromoServiceMongoDB.DataAccess.Utility;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Azure.WebJobs;
using PromoServiceMongoDB;

[assembly: WebJobsStartup(typeof(Startup))]
namespace PromoServiceMongoDB
{
    public class Startup : IWebJobsStartup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvcCore()
                 .AddJsonFormatters();

            services.AddTransient<ICosmosConnection, CosmosConnection>();
            services.AddTransient<ICosmosDataAdapter, CosmosDataAdapter>();


          // services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /* public void Configure(IApplicationBuilder app, IHostingEnvironment env)
         {
             if (env.IsDevelopment())
             {
                 app.UseDeveloperExceptionPage();
             }

             app.UseMvc();
         }*/

        public void Configure(IWebJobsBuilder builder)
          { 
            
               ConfigureServices(builder.Services); 
         }

}
}
