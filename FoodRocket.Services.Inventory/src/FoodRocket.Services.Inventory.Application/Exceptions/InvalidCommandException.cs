using System;
using Convey.CQRS.Commands;

namespace FoodRocket.Services.Inventory.Application.Exceptions
{
    public class InvalidCommandException : AppException
    {
        public override string Code { get; } = $"invalid_command_payload";
        public string CommandName { get; }
        public string Reason { get;  }

        public InvalidCommandException(string commandName, string message) : base($"Invalid command: {commandName}, reason: {message}")
        {
            CommandName = commandName;
            Reason = message;
        }

    }
}