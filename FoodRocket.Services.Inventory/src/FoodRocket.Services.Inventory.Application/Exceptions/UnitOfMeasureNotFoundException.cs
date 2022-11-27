using System;

namespace FoodRocket.Services.Inventory.Application.Exceptions
{
    public class UnitOfMeasureNotFoundException : AppException
    {
        public override string Code { get; } = "unit_of_measure_not_found";
        public long Id { get; }
        public IEnumerable<long> Ids { get; } = Enumerable.Empty<long>();

        public UnitOfMeasureNotFoundException(long id) : base($"Unit of measure (id:{id}) is not found.")
            => (Id) = (id);
        
        public UnitOfMeasureNotFoundException(IEnumerable<long> ids) : base($"Unit of measures are not found.")
            => (Ids) = (ids);
    }
}