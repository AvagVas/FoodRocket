using Convey.CQRS.Commands;
using FoodRocket.Services.Inventory.Application.Exceptions;
using FoodRocket.Services.Inventory.Application.Services;
using FoodRocket.Services.Inventory.Core.Entities.Inventory;
using FoodRocket.Services.Inventory.Core.Repositories;
using FoodRocket.Services.Inventory.Core.ValueObjects;

namespace FoodRocket.Services.Inventory.Application.Commands.Inventory.Handlers;

public class AddProductQuantityHandler : ICommandHandler<AddProductQuantity>
{
    private readonly IProductRepository _productRepository;
    private readonly IStorageRepository _storageRepository;
    private readonly IUnitOfMeasureRepository _uomRepository;
    private readonly IProductAvailabilityRepository _productAvailabilityRepository;
    private readonly INewIdGenerator _idGenerator;

    public AddProductQuantityHandler(IProductRepository productRepository, IStorageRepository storageRepository,
        IUnitOfMeasureRepository uomRepository, IProductAvailabilityRepository productAvailabilityRepository,
        INewIdGenerator idGenerator)
    {
        _productRepository = productRepository;
        _storageRepository = storageRepository;
        _uomRepository = uomRepository;
        _productAvailabilityRepository = productAvailabilityRepository;
        _idGenerator = idGenerator;
    }

    public async Task HandleAsync(AddProductQuantity command,
        CancellationToken cancellationToken = new CancellationToken())
    {
        if (command.StorageId == 0)
        {
            throw new InvalidCommandException(nameof(AddProductQuantity), "StorageId should be provided");
        }

        var storage = await _storageRepository.GetAsync(command.StorageId);
        if (storage is null)
        {
            throw new StorageNotFoundException(command.StorageId);
        }

        UnitOfMeasure? uom = null;
        if (command.UnitOfMeasureId > 0)
        {
            uom = await _uomRepository.GetAsync(command.UnitOfMeasureId);
            if (uom is null)
            {
                throw new UnitOfMeasureNotFoundException(command.UnitOfMeasureId);
            }
        }

        var product = await _productRepository.GetAsync(command.ProductId);
        if (product is null)
        {
            throw new ProductNotFoundException(command.ProductId);
        }

        var productInStorage = await _productAvailabilityRepository.GetAsync(product.Id, storage.Id);
        if (productInStorage is null)
        {
            var productQuantity = new QuantityOfProduct(product, uom!, command.Quantity);
            var newProductAvailability = new ProductAvailability(_idGenerator.GetNewIdFor(nameof(ProductAvailability)),
                productQuantity, storage);

            await _productAvailabilityRepository.AddAsync(newProductAvailability);
        }
        else
        {
            var productQuantity = new QuantityOfProduct(product, uom!, command.Quantity);
            var newProductQuantity = productInStorage.QuantityOfProduct + productQuantity;

            productInStorage.ChangeQuantity(newProductQuantity);
            await _productAvailabilityRepository.UpdateAsync(productInStorage);
        }
    }
}