using System.Text;
using Convey;
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
using FoodRocket.Services.Tornado.Application;
using FoodRocket.Services.Tornado.Application.Configurations;
using FoodRocket.Services.Tornado.Application.Events;
using FoodRocket.Services.Tornado.Application.Services;
using FoodRocket.Services.Tornado.Infrastructure.Contexts;
using FoodRocket.Services.Tornado.Infrastructure.Decorators;
using FoodRocket.Services.Tornado.Infrastructure.Exceptions;
using FoodRocket.Services.Tornado.Infrastructure.Jaeger;
using FoodRocket.Services.Tornado.Infrastructure.Logging;
using FoodRocket.Services.Tornado.Infrastructure.Metrics;
using FoodRocket.Services.Tornado.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using FoodRocket.Services.Tornado.Infrastructure.Services.Clients;
using FoodRocket.Services.Tornado.Application.Commands.Integrations.Smtp;
using FoodRocket.Services.Tornado.Application.Events.External;
using Convey.Auth;
using FoodRocket.Services.Tornado.Infrastructure.AutoMapper;
using FoodRocket.Services.Tornado.Infrastructure.SettingOptions;

namespace FoodRocket.Services.Tornado.Infrastructure
{
    public static class Extensions
    {
        private const string _emailSenderSectionName = "EmailClient";
        public static IConveyBuilder AddInfrastructure(this IConveyBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddTransient<IMessageBroker, MessageBroker>();
            builder.Services.AddTransient<IEmailClient, EmailClient>();
            builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            builder.Services.AddTransient<IAppContextFactory, AppContextFactory>();
            builder.Services.AddTransient(ctx => ctx.GetRequiredService<IAppContextFactory>().Create());
            builder.Services.AddHostedService<MetricsJob>();
            builder.Services.AddSingleton<CustomMetricsMiddleware>();
            builder.Services.TryDecorate(typeof(ICommandHandler<>), typeof(OutboxCommandHandlerDecorator<>));
            builder.Services.TryDecorate(typeof(IEventHandler<>), typeof(OutboxEventHandlerDecorator<>));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {

            });

            //builder.Services.AddSingleton<IIntegrationConfiguration>(builder.GetOptions<IntegrationConfiguration>("IntegrationConfiguration"));
            builder.Services.AddAuthorization(o =>
            {

            });
            return builder
                .AddErrorHandler<ExceptionToResponseMapper>()
                .AddQueryHandlers()
                .AddInMemoryQueryDispatcher()
                .AddHttpClient()
                .AddJwt()
                .AddConsul()
                .AddFabio()
                .AddExceptionToMessageMapper<ExceptionToMessageMapper>()
                .AddRabbitMq(plugins: p => p.AddJaegerRabbitMqPlugin())
                .AddMessageOutbox(o => o.AddMongo())
                .AddMongo()
                .AddRedis()
                .AddMetrics()
                .AddJaeger()
                .AddJaegerDecorators()
                .AddHandlersLogging()
                .AddEmailClient()
                .AddCertificateAuthentication()
                .AddSecurity();
        }


        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseErrorHandler()
                .UseSwagger()
                .UseSwaggerUI()
                .UseJaeger()
                .UseConvey()
                .UsePublicContracts<ContractAttribute>()
                .UseMetrics()
                .UseMiddleware<CustomMetricsMiddleware>()
                .UseCertificateAuthentication()
                .UseHttpsRedirection()
                .UseAuthentication()
                .UseAuthorization()
                .UseRabbitMq()
                .SubscribeEvent<SendBulkEmail>()
                .SubscribeEvent<SendEmail>();
            return app;

        }
        internal static CorrelationContext GetCorrelationContext(this IHttpContextAccessor accessor)
        {
            return accessor.HttpContext?.Request.Headers.TryGetValue("Correlation-Context", out var json) is true
                        ? JsonConvert.DeserializeObject<CorrelationContext>(json.FirstOrDefault())
                        : null;
        }

        internal static IDictionary<string, object> GetHeadersToForward(this IMessageProperties messageProperties)
        {
            const string sagaHeader = "Saga";
            if (messageProperties?.Headers is null || !messageProperties.Headers.TryGetValue(sagaHeader, out var saga))
            {
                return null;
            }

            return saga is null
                ? null
                : new Dictionary<string, object>
                {
                    [sagaHeader] = saga
                };
        }

        internal static string GetSpanContext(this IMessageProperties messageProperties, string header)
        {
            if (messageProperties is null)
            {
                return string.Empty;
            }

            if (messageProperties.Headers!=null && messageProperties.Headers.TryGetValue(header, out var span) && span != null && span is byte[] spanBytes)
            {
                return Encoding.UTF8.GetString(spanBytes);
            }

            return string.Empty;
        }
        
        static IConveyBuilder AddEmailClient(this IConveyBuilder builder)
        {
            var emailClientOptions = builder.GetOptions<EmailClientConfigurationOptions>(_emailSenderSectionName);
            builder.Services.AddSingleton(emailClientOptions);
            return builder;
        }
    }
}
