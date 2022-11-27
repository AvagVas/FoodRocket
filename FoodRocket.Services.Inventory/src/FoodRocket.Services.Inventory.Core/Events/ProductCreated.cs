using FoodRocket.Services.Inventory.Core.Entities.Inventory;

namespace FoodRocket.Services.Inventory.Core.Events
{
    public class ProductCreated : IDomainEvent
    {
        public Product Product { get; }

        public ProductCreated(Product product)
            => Product = product;
    }
}