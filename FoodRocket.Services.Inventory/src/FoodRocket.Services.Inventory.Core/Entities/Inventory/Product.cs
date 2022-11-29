using System.Collections.Concurrent;
using System.ComponentModel;
using FoodRocket.Services.Inventory.Core.Events;
using FoodRocket.Services.Inventory.Core.Exceptions;
using FoodRocket.Services.Inventory.Core.ValueObjects;

namespace FoodRocket.Services.Inventory.Core.Entities.Inventory;

public class Product : AggregateRoot
{

    private List<UnitOfMeasure> _unitOfMeasures = new ();
    public ProductName Name { get; }
    public UnitOfMeasure MainUnitOfMeasure { get; }

    public IEnumerable<UnitOfMeasure> UnitOfMeasures
    {
        get => _unitOfMeasures;
        private set => _unitOfMeasures = new List<UnitOfMeasure>(value);
    }

    public Product(long id, ProductName productName, UnitOfMeasure mainUnitOfMeasure, int version = 0)
    {
        ValidateProduct(productName, mainUnitOfMeasure);
        Id = id;
        Version = version;
        MainUnitOfMeasure = mainUnitOfMeasure;
        AddUnitOfMeasure(mainUnitOfMeasure);
    }

    public static Product Create(long id, ProductName productName, UnitOfMeasure mainUnitOfMeasure)
    {
        ValidateProduct(productName, mainUnitOfMeasure);
        var product = new Product(id, productName, mainUnitOfMeasure);
        product.AddEvent(new ProductCreated(product));
        return product;
    }

    public void Delete()
    {
        AddEvent(new ProductDeleted(this));
    }

    public void AddUnitOfMeasure(UnitOfMeasure unitOfMeasure)
    {
        var foundUofM =_unitOfMeasures.FirstOrDefault(unit => unit.Id == unitOfMeasure.Id);
        if (foundUofM is {})
        {
            // throw new UnitOfMeasureCouldNotBeAddedToProductException(unitOfMeasure, this);
            return;
        }

        if (unitOfMeasure.TypeOfUnitOfMeasure != MainUnitOfMeasure.TypeOfUnitOfMeasure)
        {
            throw new UnitOfMeasureCouldNotBeAddedToProductException(unitOfMeasure, this);
        }

        _unitOfMeasures.Add(unitOfMeasure);
    }

    public void AddUnitOfMeasure(IEnumerable<UnitOfMeasure> unitOfMeasures)
    {
        foreach (var eachUom in unitOfMeasures)
        {
            AddUnitOfMeasure(eachUom);
        }
    }

    public void RemoveUnitOfMeasure(UnitOfMeasure unitOfMeasure)
    {
        var foundUofM =_unitOfMeasures.FirstOrDefault(unit => unit.Id == unitOfMeasure.Id);
        if (foundUofM is {})
        {
            _unitOfMeasures.RemoveAll(unit => unit.Id == unitOfMeasure.Id);
        }
    }

    private static void ValidateProduct(ProductName productName, UnitOfMeasure mainUnitOfMeasure)
    {
        // currently there is nothing to validate. Will become clear a bit later
    }
}