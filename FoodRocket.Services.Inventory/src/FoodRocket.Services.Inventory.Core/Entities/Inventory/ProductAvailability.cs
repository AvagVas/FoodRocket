using System.ComponentModel;
using FoodRocket.Services.Inventory.Core.Events;
using FoodRocket.Services.Inventory.Core.Exceptions;
using FoodRocket.Services.Inventory.Core.ValueObjects;

namespace FoodRocket.Services.Inventory.Core.Entities.Inventory;

public class ProductAvailability
{
    public long Id;
    public QuantityOfProduct QuantityOfProduct { get; private set; }
    public Storage Storage { get; }

    
    public ProductAvailability(long id, QuantityOfProduct quantity, Storage storage)
    {
        Id = id;
        QuantityOfProduct = quantity;
        Storage = storage;
    }

    public void ChangeQuantity(QuantityOfProduct newQuantity)
    {
        QuantityOfProduct = newQuantity;
    }
}