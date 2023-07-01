using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Compact;
using Serilog.Settings.Configuration;
using System.Reflection;

namespace Identity.Infrastructure.Logging;

internal static class Configure
{
    public static string RegisterLogger(this WebApplicationBuilder builder)
    {
        string appName = Assembly.GetEntryAssembly()?.GetName().Name!;

        var isDevelopmentEnvironment = builder.Environment.IsDevelopment();
        builder.Host.UseSerilog((context, serviceProvider, loggerConfig) =>
        {
            loggerConfig
                .ReadFrom.Configuration(builder.Configuration, new ConfigurationReaderOptions
                {
                    SectionName = "SerilogSettings"
                })
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", appName)
                .Enrich.WithExceptionDetails()
                .Enrich.WithMachineName()
                .Enrich.WithProcessId()
                .Enrich.WithThreadId()
                .Enrich.FromLogContext()
                .WriteTo.Console(/*restrictedToMinimumLevel: LogEventLevel.Warning*/);

            if (isDevelopmentEnvironment)
            {
                loggerConfig.WriteTo.File(
                    new CompactJsonFormatter(),
                    "Logs/logs.json",
                    restrictedToMinimumLevel: LogEventLevel.Information,
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 5);
            }

            loggerConfig
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Error);
        });

        return appName;
    }
}
