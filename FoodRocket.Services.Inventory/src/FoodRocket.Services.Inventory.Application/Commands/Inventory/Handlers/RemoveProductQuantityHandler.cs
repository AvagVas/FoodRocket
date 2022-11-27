using Convey.CQRS.Commands;
using FoodRocket.Services.Inventory.Application.Exceptions;
using FoodRocket.Services.Inventory.Application.Services;
using FoodRocket.Services.Inventory.Core.Entities.Inventory;
using FoodRocket.Services.Inventory.Core.Repositories;
using FoodRocket.Services.Inventory.Core.ValueObjects;

namespace FoodRocket.Services.Inventory.Application.Commands.Inventory.Handlers;

public class RemoveProductQuantityHandler : ICommandHandler<AddProductQuantity>
{
    private readonly IProductRepository _productRepository;
    private readonly IStorageRepository _storageRepository;
    private readonly IUnitOfMeasureRepository _uomRepository;
    private readonly IProductAvailabilityRepository _productAvailabilityRepository;
    private readonly INewIdGenerator _idGenerator;

    public RemoveProductQuantityHandler(IProductRepository productRepository, IStorageRepository storageRepository,
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
            throw new NoSufficientQuantityInStorageException(product, storage, command.Quantity, uom!);
        }

        var quantityToSubtract = new QuantityOfProduct(product, uom!, command.Quantity);
        var quantityInStorage = productInStorage.QuantityOfProduct;
        if (quantityInStorage < quantityToSubtract)
        {
            throw new NoSufficientQuantityInStorageException(product, storage, command.Quantity, uom!);
        }

        var difference = quantityInStorage - quantityToSubtract;
        productInStorage.ChangeQuantity(difference);
        await _productAvailabilityRepository.UpdateAsync(productInStorage);

    }
}