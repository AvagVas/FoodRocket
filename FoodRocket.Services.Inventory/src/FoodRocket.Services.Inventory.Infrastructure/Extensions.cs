using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Convey;
using Convey.Auth;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using Convey.CQRS.Queries;
using Convey.Discovery.Consul;
using Convey.Docs.Swagger;
using Convey.HTTP;
using Convey.LoadBalancing.Fabio;
using Convey.MessageBrokers;
using Convey.MessageBrokers.CQRS;
using Convey.MessageBrokers.Outbox;
using Convey.MessageBrokers.Outbox.Mongo;
using Convey.MessageBrokers.RabbitMQ;
using Convey.Metrics.AppMetrics;
using Convey.Persistence.MongoDB;
using Convey.Persistence.Redis;
using Convey.Security;
using Convey.Tracing.Jaeger;
using Convey.Tracing.Jaeger.RabbitMQ;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using Convey.WebApi.Security;
using Convey.WebApi.Swagger;
using Elasticsearch.Net;
using FoodRocket.DBContext.Contexts;
using FoodRocket.Services.Identity.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using FoodRocket.Services.Inventory.Application;
using FoodRocket.Services.Inventory.Application.Commands.Inventory;
using FoodRocket.Services.Inventory.Application.Events;
using FoodRocket.Services.Inventory.Application.Events.Customers.External;
using FoodRocket.Services.Inventory.Application.Events.Inventory.External;
using FoodRocket.Services.Inventory.Application.Services;
using FoodRocket.Services.Inventory.Application.Services.Clients;
using FoodRocket.Services.Inventory.Core.Repositories;
using FoodRocket.Services.Inventory.Core.Repositories.Customers;
using FoodRocket.Services.Inventory.Infrastructure.Contexts;
using FoodRocket.Services.Inventory.Infrastructure.Decorators;
using FoodRocket.Services.Inventory.Infrastructure.Exceptions;
using FoodRocket.Services.Inventory.Infrastructure.Jaeger;
using FoodRocket.Services.Inventory.Infrastructure.Logging;
using FoodRocket.Services.Inventory.Infrastructure.Metrics;
using FoodRocket.Services.Inventory.Infrastructure.Services;
using FoodRocket.Services.Inventory.Infrastructure.Services.Clients;
using FoodRocket.Services.Inventory.Infrastructure.SettingOptions;
using FoodRocket.Services.Inventory.Infrastructure.SqlServer.Customers.Repositories;
using FoodRocket.Services.Inventory.Infrastructure.SqlServer.Inventory.Repositories;
using FoodRocket.Services.Inventory.Infrastructure.SqlServer.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FoodRocket.Services.Inventory.Infrastructure
{
    public static class Extensions
    {
        private const string _IDGeneratosrSectionName = "IDGenerators";
        public static IConveyBuilder AddInfrastructure(this IConveyBuilder builder)
        {
            builder.Services.AddSingleton<IEventMapper, EventMapper>();
            builder.Services.AddTransient<IMessageBroker, MessageBroker>();
            builder.Services.AddSingleton<INewIdGenerator, NewIdGenerator>();
            builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            builder.Services.AddTransient<IAppContextFactory, AppContextFactory>();
            builder.Services.AddTransient<IEventProcessor, EventProcessor>();
            builder.Services.AddTransient(ctx => ctx.GetRequiredService<IAppContextFactory>().Create());
            builder.Services.AddHostedService<MetricsJob>();
            builder.Services.AddSingleton<CustomMetricsMiddleware>();
            builder.Services.TryDecorate(typeof(ICommandHandler<>), typeof(OutboxCommandHandlerDecorator<>));
            builder.Services.TryDecorate(typeof(IEventHandler<>), typeof(OutboxEventHandlerDecorator<>));
            builder.Services.Scan(s => s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                .AddClasses(c => c.AssignableTo(typeof(IDomainEventHandler<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            return builder
                .AddErrorHandler<ExceptionToResponseMapper>()
                .AddQueryHandlers()
                .AddInMemoryQueryDispatcher()
                .AddJwt()
                .AddHttpClient()
                .AddConsul()
                .AddFabio()
                .AddRabbitMq(plugins: p => p.AddJaegerRabbitMqPlugin())
                .AddMessageOutbox(o => o.AddMongo())
                .AddExceptionToMessageMapper<ExceptionToMessageMapper>()
                .AddMongo()
                .AddSqlServer()
                .AddRedis()
                .AddMetrics()
                .AddJaeger()
                .AddJaegerDecorators()
                .AddHandlersLogging()
                //.AddS()
                //.AddCertificateAuthentication()
                .AddIDGenerator()
                .AddSecurity();
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseErrorHandler()
                .UseJaeger()
                .UseConvey()
                .UsePublicContracts<ContractAttribute>()
                .UseMetrics()
                .UseAccessTokenValidator()
                .UseAuthentication()
                 //.UseCertificateAuthentication()
                .UseMiddleware<CustomMetricsMiddleware>()
                .UseRabbitMq()
                .SubscribeCommand<AddProduct>()
                .SubscribeEvent<SignedUp>();
            return app;
        }

        internal static CorrelationContext? GetCorrelationContext(this IHttpContextAccessor accessor)
            => accessor.HttpContext?.Request.Headers.TryGetValue("Correlation-Context", out var json) is true
                ? JsonConvert.DeserializeObject<CorrelationContext>(json.FirstOrDefault() ?? "")
                : null;

        internal static IDictionary<string, object>? GetHeadersToForward(this IMessageProperties messageProperties)
        {
            const string sagaHeader = "Saga";
            if (messageProperties?.Headers is null || !messageProperties.Headers.TryGetValue(sagaHeader, out var saga))
            {
                return null;
            }

            return (saga is null
                ? null
                : new Dictionary<string, object>
                {
                    [sagaHeader] = saga
                }) ?? throw new InvalidOperationException();
        }

        internal static string GetSpanContext(this IMessageProperties? messageProperties, string header)
        {
            if (messageProperties is null)
            {
                return string.Empty;
            }

            if (messageProperties.Headers.TryGetValue(header, out var span) && span is byte[] spanBytes)
            {
                return Encoding.UTF8.GetString(spanBytes);
            }

            return string.Empty;
        }
        
        static IConveyBuilder AddIDGenerator(this IConveyBuilder builder)
        {
            var iDGeneratorOptions = builder.GetOptions<IDGeneratorConfigurationOptions>(_IDGeneratosrSectionName);
            builder.Services.AddSingleton(iDGeneratorOptions);
            return builder;
        }
        
        static IConveyBuilder AddSqlServer(this IConveyBuilder builder)
        {
            using var provider = builder.Services.BuildServiceProvider();
            var configuration = provider.GetService<IConfiguration>();

            #region Inventory

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductAvailabilityRepository, ProductAvailabilityRepository>();
            builder.Services.AddScoped<IStorageRepository, StorageRepository>();
            builder.Services.AddScoped<IUnitOfMeasureRepository, UnitOfMeasureRepository>();

            #endregion // Inventory

            #region Customer

            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

            #endregion

            var options = configuration.GetOptions<SqlServerOptions>("SqlServer");
            builder.Services.AddDbContext<InventoryDbContext>(ctx =>
            {
                ctx.UseSqlServer(options.ConnectionString);
                ctx.EnableSensitiveDataLogging();
            });

            builder.Services.AddDbContext<CustomersDbContext>(ctx => 
            {
                ctx.UseSqlServer(options.ConnectionString);
                ctx.EnableSensitiveDataLogging();
            });

            builder.Services.AddDbContext<OrdersDbContext>(ctx => 
            {
                ctx.UseSqlServer(options.ConnectionString);
                ctx.EnableSensitiveDataLogging();
            });
            
            builder.Services.AddDbContext<StaffDbContext>(ctx => 
            {
                ctx.UseSqlServer(options.ConnectionString);
                ctx.EnableSensitiveDataLogging();
            });


            return builder;
        }
    }
}