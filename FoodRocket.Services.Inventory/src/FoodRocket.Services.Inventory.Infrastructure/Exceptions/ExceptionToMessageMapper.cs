using System;
using Convey.MessageBrokers.RabbitMQ;
using FoodRocket.Services.Inventory.Application.Commands;
//using FoodRocket.Services.Inventory.Application.Events.Rejected;
using FoodRocket.Services.Inventory.Application.Exceptions;
using FoodRocket.Services.Inventory.Core.Exceptions;

namespace FoodRocket.Services.Inventory.Infrastructure.Exceptions
{
    internal sealed class ExceptionToMessageMapper : IExceptionToMessageMapper
    {
        public object Map(Exception exception, object message)
            => (exception switch
            {
                // TODO VAS: Add mapping from exception of Application to Messages
                _ => null
            })!;
    }
}