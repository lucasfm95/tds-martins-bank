using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MartinsBankApi.Middlewares
{
    public static class SwaggerMeddleware
    {
        public static void AddSwaggerMiddleware(this IServiceCollection services )
        {
            services.AddSwaggerGen( ( a ) =>
            {
                a.SwaggerDoc( "v1", new Info
                {
                    Title = "Martins Bank",
                    Version = "v1",
                    Description = "Martins Bank API"
                } );

                var appPath = PlatformServices.Default.Application.ApplicationBasePath;

                var xmlDocPath = Path.Combine( appPath, "MartinsBankApi.xml" );

                a.IncludeXmlComments( xmlDocPath );
            } );
        }

        public static void UseSwaggerMiddleware( this IApplicationBuilder app )
        {
            app.UseSwagger( );

            app.UseSwaggerUI((a) => 
            {
                a.SwaggerEndpoint( "/swagger/v1/swagger.json", "MartinsBankApi" );

                a.RoutePrefix = "docs";
            } );
        }
    }
}
