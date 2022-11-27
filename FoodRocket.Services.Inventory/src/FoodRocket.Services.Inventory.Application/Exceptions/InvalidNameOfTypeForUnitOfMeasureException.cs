using System;

namespace FoodRocket.Services.Inventory.Application.Exceptions
{
    public class InvalidNameOfTypeForUnitOfMeasureException : AppException
    {
        public override string Code { get; } = "invalid_name_of_type_for_uom";
        public string Name { get; }

        public InvalidNameOfTypeForUnitOfMeasureException(string name) : base($"Invalid name {name} Type For UoM.")
            => (Name) = (name);
    }
}