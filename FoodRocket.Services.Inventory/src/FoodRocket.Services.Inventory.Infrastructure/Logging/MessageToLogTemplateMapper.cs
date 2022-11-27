using System;
using System.Collections.Generic;
using Convey.Logging.CQRS;
using FoodRocket.Services.Inventory.Application.Commands;
using FoodRocket.Services.Inventory.Application.Events.Inventory.External;
using FoodRocket.Services.Inventory.Application.Exceptions;

namespace FoodRocket.Services.Inventory.Infrastructure.Logging
{
    internal sealed class MessageToLogTemplateMapper : IMessageToLogTemplateMapper
    {
        private static IReadOnlyDictionary<Type, HandlerLogTemplate> MessageTemplates 
            => new Dictionary<Type, HandlerLogTemplate>
            {
 
            };
        
        public HandlerLogTemplate? Map<TMessage>(TMessage message) where TMessage : class
        {
            var key = message.GetType();
            return MessageTemplates.TryGetValue(key, out var template) ? template : null;
        }
    }
}