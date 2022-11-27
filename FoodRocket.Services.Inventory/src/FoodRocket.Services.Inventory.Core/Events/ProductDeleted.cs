using FoodRocket.Services.Inventory.Core.Entities.Inventory;

namespace FoodRocket.Services.Inventory.Core.Events
{
    public class ProductDeleted : IDomainEvent
    {
        public Product Product { get; }

        public ProductDeleted(Product product)
            => Product = product;
    }
}