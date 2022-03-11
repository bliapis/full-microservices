using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Reflection;

namespace Common.Logging
{
    public static class SeriLogger
    {
        public static Action<HostBuilderContext, LoggerConfiguration> Configure =>
            (context, configuration) =>
            {
                configuration
                     .Enrich.FromLogContext()
                     .Enrich.WithMachineName()
                     .WriteTo.Console()
                     .WriteTo.Elasticsearch(
                         new ElasticsearchSinkOptions(
                             new Uri(context.Configuration["ElasticConfiguration:Uri"]))
                         {
                             IndexFormat = $"applogs-{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-logs-{DateTime.UtcNow:yyyy-MM}",
                             AutoRegisterTemplate = true,
                             NumberOfShards = 2,
                             NumberOfReplicas = 1
                         })
                     .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                     .ReadFrom.Configuration(context.Configuration);
            };
    }
}