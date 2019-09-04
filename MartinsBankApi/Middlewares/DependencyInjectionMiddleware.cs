using MartinsBank.Repository;
using MartinsBank.Repository.Context;
using MartinsBank.Repository.Context.Interfaces;
using MartinsBank.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartinsBankApi.Middlewares
{
    public static class DependencyInjectionMiddleware
    {
        public static void AddDependencyInjection( this IServiceCollection services, IConfiguration configuration )
        {
            string connectionString = configuration.GetConnectionString( "DefaultConnection" );

            IConnectionFactory connectionFactory = new ConnectionFactory( connectionString );

            services.AddSingleton<IConnectionFactory>( connectionFactory );

            services.AddSingleton<IAccountEventRepository, AccountEventRepository>( );
        }
    }
}
