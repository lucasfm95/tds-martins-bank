using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MartinsBankApi.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MartinsBankApi
{
    public class Startup
    {
        public Startup( IConfiguration configuration )
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices( IServiceCollection services )
        {
            services.AddMvc( ).SetCompatibilityVersion( CompatibilityVersion.Version_2_2 );

            // Adiciona os middlewares
            services.AddDependencyInjection( Configuration );
            services.AddSwaggerMiddleware( );
        }

        public void Configure( IApplicationBuilder app, IHostingEnvironment env )
        {
            if ( env.IsDevelopment( ) )
            {
                app.UseDeveloperExceptionPage( );
            }

            app.UseMvc( );
            app.UseSwaggerMiddleware( );
        }
    }
}
