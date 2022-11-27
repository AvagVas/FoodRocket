using System;

namespace FoodRocket.Services.Inventory.Application.Exceptions
{
    public class InitialProductAvailabilityCantBeAddedException : AppException
    {
        public override string Code { get; } = "initial_product_availability_cant_be_added";

        public InitialProductAvailabilityCantBeAddedException() : base($"Initial product availability can't be added.")
        {
            
        }
    }
}