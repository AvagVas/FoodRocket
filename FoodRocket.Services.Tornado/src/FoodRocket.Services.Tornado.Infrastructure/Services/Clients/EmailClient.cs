using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;

using FoodRocket.Services.Tornado.Application.Configurations;
using FoodRocket.Services.Tornado.Application.Enums;
using FoodRocket.Services.Tornado.Application.Events;
using FoodRocket.Services.Tornado.Application.Exceptions.Integrations;
using FoodRocket.Services.Tornado.Application.Services;
using FoodRocket.Services.Tornado.Application.ValueObject;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;
using System.Text;
using FoodRocket.Services.Tornado.Infrastructure.SettingOptions;

namespace FoodRocket.Services.Tornado.Infrastructure.Services.Clients
{
    public class EmailClient : IEmailClient
    {
        private readonly IMessageBroker _messageBroker;
        private readonly ILogger<EmailClient> _logger;
        private readonly EmailClientConfigurationOptions _configuration;
        public EmailClient(EmailClientConfigurationOptions configuration, IMessageBroker messageBroker, ILogger<EmailClient> logger)
        {
            var path = Environment.GetEnvironmentVariable("FOOD_ROCKET_EMAIL_ITEMS_PICKUP_PATH");
            _configuration = configuration;
            _messageBroker = messageBroker;
            _logger = logger;
            Initialization();
        }

        public async Task<bool> SendEmailBasedOnTemplateAsync(string subject, StringBuilder template, object model, string from, string to, string receiverName)
        {
            try
            {
                Email.DefaultSender = GetNewSender();
                Email.DefaultRenderer = new RazorRenderer();

                await Email
                    .From(_configuration.From)
                    .To(to, receiverName)
                    .Subject(subject)
                    .UsingTemplate(template.ToString(), model)
                    .SendAsync();

                return true;
            }
            catch (Exception ex)
            {
                EmailNotSend emailNotSend = new()
                {
                    Code =to,
                    Reason = ex.Message,
                    Type = nameof(ChannelTypes.Email),
                    Email = to
                };
                throw new SendEmailException(new List<EmailNotSend>() { emailNotSend });
            }
        }
        
        public async Task<bool> SendEmailAsync(string subject, string body, string from, string to, string receiverName)
        {
            try
            {
                Email.DefaultSender = GetNewSender();;
                Email.DefaultRenderer = new RazorRenderer();

                await Email
                    .From(_configuration.From)
                    .To(to, receiverName)
                    .Subject(subject)
                    .Body(body)
                    .SendAsync();

                return true;
            }
            catch (Exception ex)
            {
                EmailNotSend emailNotSend = new()
                {
                    Code =to,
                    Reason = ex.Message,
                    Type = nameof(ChannelTypes.Email),
                    Email = to
                };
                throw new SendEmailException(new List<EmailNotSend>() { emailNotSend });
            }
        }

        public async Task<bool> SendBulkEmailAsync(string subject, string body, 
            string from, List<string> addresses)
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                // throw new SendEmailException(new List<MessageNotSend>() { messageNotSend });
                _logger.LogInformation($"Still ignore it: {ex.Message}");
                return false;
            }
        }
        private SmtpSender GetNewSender()
        {
            return new SmtpSender(() => new SmtpClient(_configuration.SmtpClientHost)
            {
                EnableSsl = false,
                //DeliveryMethod = SmtpDeliveryMethod.Network,
                //Port = 25
                DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                PickupDirectoryLocation = _configuration.EmailItemsPath
            });
        }

        private void Initialization()
        {
            if (!Directory.Exists(_configuration.EmailItemsPath))
            {
                Directory.CreateDirectory(_configuration.EmailItemsPath);
            }
        }
    }

}
