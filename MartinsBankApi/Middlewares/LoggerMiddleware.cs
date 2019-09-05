using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartinsBankApi.Middlewares
{
    public static class LoggerMiddleware
    {
        public static void AddLoggerMiddleware( this IServiceCollection services )
        {
            Console.WriteLine( );
            Log.Logger = new LoggerConfiguration( )
                .MinimumLevel.Information( )
                .MinimumLevel.Override( "Microsoft", LogEventLevel.Information )
                .Enrich.FromLogContext( )
                .WriteTo.Console( )
                .WriteTo.File( $@".\log\log.txt", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: LogEventLevel.Error )
                .CreateLogger( );
        }
    }
}
