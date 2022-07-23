using FluentMigrator.Runner;
using job_offer.API.Infra.Persistence.Migrations.MySQL;
using job_offer.JobOffers.Messages.Commands;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace job_offer.JobOffers.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "job-offer.JobOffers.API";
            var serviceProvider = CreateServices();
            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
            CreateHostBuilder(args).Build().Run();
        }


        private static IServiceProvider CreateServices()
        {
            string stringConnection = Environment.GetEnvironmentVariable("MYSQL_UNMSM_BANKING_CORE_DDD");
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .WithGlobalCommandTimeout(new TimeSpan(1, 0, 0))
                    .AddMySql5()
                    .WithGlobalConnectionString(stringConnection)
                    .ScanIn(typeof(CreateInitialschema).Assembly)
                    .For.All()
                )
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseMicrosoftLogFactoryLogging()
                .UseNServiceBus(hostBuilderContext =>
                {
                    var endPointName = "job-offer.JobOffers.API";
                    string rabbitmqUrl = Environment.GetEnvironmentVariable("RABBITMQ_UNMSM_BANKING_CORE_DDD");
                    var endpointConfiguration = new EndpointConfiguration(endPointName);
                    var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
                    transport.ConnectionString(rabbitmqUrl);
                    transport.UseConventionalRoutingTopology();
                    var routing = transport.Routing();
                    routing.RouteToEndpoint(typeof(CreateJobOffer).Assembly, "job-offer.JobOffers");
                    endpointConfiguration.SendOnly();
                    return endpointConfiguration;
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
