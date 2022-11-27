using System;

namespace FoodRocket.Services.Inventory.Application.Exceptions
{
    public class UnitOfMeasureAlreadyExistsException : AppException
    {
        public override string Code { get; } = "unit_of_measure_already_exists";
        public long Id { get; }

        public UnitOfMeasureAlreadyExistsException(long id) : base($"Unit of measure {id} already exists.")
            => (Id) = (id);
    }
}