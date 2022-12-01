using System.Collections.Concurrent;
using FoodRocket.DBContext.Models.Customers;
using FoodRocket.DBContext.Models.Inventory;
using FoodRocket.Services.Inventory.Application.Exceptions;
using FoodRocket.Services.Inventory.Core.Exceptions;
using FoodRocket.Services.Inventory.Core.ValueObjects;
using Product = FoodRocket.Services.Inventory.Core.Entities.Inventory.Product;
using ProductDb = FoodRocket.DBContext.Models.Inventory.Product;
using Storage = FoodRocket.Services.Inventory.Core.Entities.Inventory.Storage;
using StorageDb = FoodRocket.DBContext.Models.Inventory.Storage;
using UnitOfMeasure = FoodRocket.Services.Inventory.Core.Entities.Inventory.UnitOfMeasure;
using UnitOfMeasureDb = FoodRocket.DBContext.Models.Inventory.UnitOfMeasure;
using TypeOfUnitOfMeasure = FoodRocket.Services.Inventory.Core.ValueObjects.TypeOfUnitOfMeasure;
using TypeOfUnitOfMeasureDb = FoodRocket.DBContext.Models.Inventory.TypeOfUnitOfMeasure;
using ProductAvailability = FoodRocket.Services.Inventory.Core.Entities.Inventory.ProductAvailability;
using StorageProductDb = FoodRocket.DBContext.Models.Inventory.StorageProduct;

namespace FoodRocket.Services.Inventory.Infrastructure.SqlServer.Inventory;

internal static class Extensions
{
    public static Storage AsEntity(this StorageDb storageDb)
    {
        return new Storage(storageDb.StorageId, storageDb.Name, storageDb.ManagerId ?? 0);
    }

    public static List<Storage> AsEntity(this IEnumerable<StorageDb> storageDbs)
    {
        List<Storage> storages = new();

        foreach (var storageDb in storageDbs)
        {
            storages.Add(storageDb.AsEntity());
        }

        return storages;
    }

    public static ProductAvailability AsEntity(this StorageProductDb storageProductDb, IEnumerable<UnitOfMeasureDb> baseUnitOfMeasures)
    {
        var listOfBaseUnitOfMeasures = baseUnitOfMeasures.ToList();
        QuantityOfProduct quantityOfProduct = new(
            storageProductDb.Product.AsEntity(listOfBaseUnitOfMeasures),
            storageProductDb.UnitOfMeasure.AsEntity(listOfBaseUnitOfMeasures),
            storageProductDb.Quantity);
        var productAvailability = new ProductAvailability(0, quantityOfProduct, storageProductDb.Storage.AsEntity());
        return productAvailability;
    }

    public static List<ProductAvailability> AsEntity(this IEnumerable<StorageProductDb> storageProductDbs, IEnumerable<UnitOfMeasureDb> baseUnitOfMeasures)
    {
        List<ProductAvailability> listOfProductAvailabilities = new();
        var listOfBaseUnitOfMeasures = baseUnitOfMeasures.ToList();
        foreach (var storageProductDb in storageProductDbs)
        {
            listOfProductAvailabilities.Add(storageProductDb.AsEntity(listOfBaseUnitOfMeasures));
        }

        return listOfProductAvailabilities;
    }

    // public static Product AsEntity(this ProductDb productDb, IEnumerable<UnitOfMeasureDb> baseUnitOfMeasures)
    // {
    //     List<UnitOfMeasureDb> listUnitOfMeasureDb = baseUnitOfMeasures.ToList();
    //     
    //     Pro
    //
    //
    //     return product;
    // }

    public static List<Product> AsEntity(this IEnumerable<ProductDb> productsDb,
        IEnumerable<UnitOfMeasureDb> baseUnitOfMeasures)
    {
        List<Product> products = new();
        List<UnitOfMeasureDb> listUnitOfMeasureDb = baseUnitOfMeasures.ToList();
        foreach (var productDb in productsDb)
        {
            products.Add(productDb.AsEntity(listUnitOfMeasureDb));
        }

        return products;
    }

    public static UnitOfMeasure AsEntity(this UnitOfMeasureDb unitOfMeasureDb,
        IEnumerable<UnitOfMeasureDb> baseUnitOfMeasures)
    {
        List<UnitOfMeasureDb> listUnitOfMeasureDb = baseUnitOfMeasures.ToList();
        TypeOfUnitOfMeasure typeOfUnitOfMeasure;
        if (!Enum.TryParse(unitOfMeasureDb.Type.ToString(), true, out typeOfUnitOfMeasure))
        {
            throw new InvalidNameOfTypeForUnitOfMeasureException(unitOfMeasureDb.Type.ToString());
        }

        UnitOfMeasure? baseUnitOfMeasure = null;

        if (unitOfMeasureDb.BaseOfUnitOfMId > 0 && listUnitOfMeasureDb.Any())
        {
            var baseUnitOfMeasureDb =
                listUnitOfMeasureDb.FirstOrDefault(item => item.UnitOfMeasureId == unitOfMeasureDb.BaseOfUnitOfMId);
            if (baseUnitOfMeasureDb is null)
            {
                throw new UnitOfMeasureNotFoundException(unitOfMeasureDb.BaseOfUnitOfMId.Value);
            }

            baseUnitOfMeasure = baseUnitOfMeasureDb.AsEntity(listUnitOfMeasureDb);
        }

        var unitOfMeasure = new UnitOfMeasure(
            unitOfMeasureDb.UnitOfMeasureId,
            typeOfUnitOfMeasure,
            unitOfMeasureDb.Name,
            unitOfMeasureDb.IsBase,
            baseUnitOfMeasure,
            unitOfMeasureDb.Ratio,
            unitOfMeasureDb.IsFractional
        );

        return unitOfMeasure;
    }

    public static List<UnitOfMeasure> AsEntity(this IEnumerable<UnitOfMeasureDb> unitOfMeasuresDb,
        IEnumerable<UnitOfMeasureDb> baseUnitOfMeasuresDb)
    {
        var unitOfMeasures = new List<UnitOfMeasure>();
        List<UnitOfMeasureDb> listUnitOfMeasureDb = baseUnitOfMeasuresDb.ToList();

        foreach (var unitOfMeasureDb in unitOfMeasuresDb)
        {
            unitOfMeasures.Add(unitOfMeasureDb.AsEntity(listUnitOfMeasureDb));
        }

        return unitOfMeasures;
    }

    public static Product AsEntity(this ProductDb productDb, IEnumerable<UnitOfMeasureDb> baseUnitOfMeasuresDb)
    {
 
        List<UnitOfMeasureDb> listBaseUnitOfMeasureDb = baseUnitOfMeasuresDb.ToList();

        Product product = new Product(productDb.ProductId, productDb.Name, productDb.MainUnitOfMeasure.AsEntity(listBaseUnitOfMeasureDb), 0);

        foreach (var productUnitOfMeasureDb in productDb.UnitOfMeasuresLink)
        {
            product.AddUnitOfMeasure(productUnitOfMeasureDb.UnitOfMeasure.AsEntity(listBaseUnitOfMeasureDb));
        }


        return product;
    }

    // As Db Models

    public static ProductDb AsDbModel(this Product product)
    {
        UnitOfMeasureDb mainUnitOfMeasureDb = product.MainUnitOfMeasure.AsDbModel();
        ProductDb productDb = new ProductDb();
        productDb.ProductId = product.Id;
        productDb.Name = product.Name;
        productDb.MainUnitOfMeasure = mainUnitOfMeasureDb;
        var listOfUnitOfMeasureDbs = product.UnitOfMeasures.AsDbModel();
        
        List<ProductUnitOfMeasure> productUnitOfMeasures = new();
        foreach (var unitOfMeasureDb in listOfUnitOfMeasureDbs)
        {
            ProductUnitOfMeasure productUnitOfMeasure = new();
            productUnitOfMeasure.ProductId = product.Id;
            productUnitOfMeasure.Product = productDb;
            productUnitOfMeasure.UnitOfMeasureId = unitOfMeasureDb.UnitOfMeasureId;
            productUnitOfMeasure.UnitOfMeasure = unitOfMeasureDb;
            productUnitOfMeasures.Add(productUnitOfMeasure);
        }

        productDb.UnitOfMeasuresLink = productUnitOfMeasures;

        return productDb;
    }

    public static List<ProductDb> AsDbModel(this IEnumerable<Product> products)
    {
        List<ProductDb> listOfUnitOfMeasureDbs = new List<ProductDb>();
        foreach (var product in products)
        {
            listOfUnitOfMeasureDbs.Add(product.AsDbModel());
        }

        return listOfUnitOfMeasureDbs;
    }

    public static UnitOfMeasureDb AsDbModel(this UnitOfMeasure unitOfMeasure)
    {
        UnitOfMeasureDb mainUnitOfMeasureDb = new UnitOfMeasureDb();
        mainUnitOfMeasureDb.UnitOfMeasureId = unitOfMeasure.Id;
        mainUnitOfMeasureDb.Name = unitOfMeasure.Name;

        if (!Enum.TryParse(unitOfMeasure.TypeOfUnitOfMeasure.ToString(), true, out TypeOfUnitOfMeasureDb typeOfUnitOfMeasureDb))
        {
            throw new InvalidNameOfTypeForUnitOfMeasureException(unitOfMeasure.TypeOfUnitOfMeasure.ToString());
        }

        mainUnitOfMeasureDb.Type = typeOfUnitOfMeasureDb;
        mainUnitOfMeasureDb.IsBase = unitOfMeasure.IsBase;
        mainUnitOfMeasureDb.Ratio = unitOfMeasure.Ratio;
        mainUnitOfMeasureDb.IsFractional = unitOfMeasure.IsFractional;
        mainUnitOfMeasureDb.BaseOfUnitOfMId = unitOfMeasure.BaseOfUnitOfM?.Id;
        return mainUnitOfMeasureDb;
    }

    public static List<UnitOfMeasureDb> AsDbModel(this IEnumerable<UnitOfMeasure> unitOfMeasures)
    {
        List<UnitOfMeasureDb> listOfUnitOfMeasureDbs = new List<UnitOfMeasureDb>();
        foreach (var unitOfMeasureDb in unitOfMeasures)
        {
            listOfUnitOfMeasureDbs.Add(unitOfMeasureDb.AsDbModel());
        }

        return listOfUnitOfMeasureDbs;
    }

    public static StorageDb AsDbModel(this Storage storage)
    {
        StorageDb storageDb = new();
        storageDb.StorageId = storage.Id;
        storageDb.Name = storage.Name;
        storageDb.ManagerId = storage.ManagerId;

        return storageDb;
    }

    public static List<StorageDb> AsDbModel(this IEnumerable<Storage> storages)
    {
        List<StorageDb> storageDbs = new();
        foreach (var storage in storages)
        {
            storageDbs.Add(storage.AsDbModel());
        }

        return storageDbs;
    }
    
    public static StorageProductDb AsDbModel(this ProductAvailability productAvailability)
    {
        StorageProductDb storageProduct = new();
        storageProduct.ProductId = productAvailability.QuantityOfProduct.Product.Id;
        storageProduct.Product = productAvailability.QuantityOfProduct.Product.AsDbModel();
        storageProduct.StorageId = productAvailability.Storage.Id;
        storageProduct.Storage = productAvailability.Storage.AsDbModel();
        storageProduct.UnitOfMeasure = productAvailability.QuantityOfProduct.UnitOfMeasure.AsDbModel();
        storageProduct.Quantity = productAvailability.QuantityOfProduct.Quantity;
        return storageProduct;
    }

    public static List<StorageProductDb> AsDbModel(this IEnumerable<ProductAvailability> productAvailabilities)
    {
        List<StorageProductDb> storageProducts = new();
        foreach (var productAvailability in productAvailabilities)
        {
            storageProducts.Add(productAvailability.AsDbModel());
        }

        return storageProducts;
    }
}