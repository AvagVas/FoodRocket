using Convey.CQRS.Commands;
using FoodRocket.Services.Inventory.Application.Exceptions;
using FoodRocket.Services.Inventory.Application.Services;
using FoodRocket.Services.Inventory.Core.Entities.Inventory;
using FoodRocket.Services.Inventory.Core.Repositories;
using FoodRocket.Services.Inventory.Core.ValueObjects;

namespace FoodRocket.Services.Inventory.Application.Commands.Inventory.Handlers;

public class AddProductHandler : ICommandHandler<AddProduct>
{
    private readonly IProductRepository _repository;
    private readonly IUnitOfMeasureRepository _uomRepository;
    private readonly IProductAvailabilityRepository _productAvailabilityRepository;
    private readonly IStorageRepository _storageRepository;
    private readonly IEventProcessor _eventProcessor;
    private readonly INewIdGenerator _idGenerator;

    public AddProductHandler(IProductRepository repository,
        IUnitOfMeasureRepository uomRepository,
        IProductAvailabilityRepository productAvailabilityRepository,
        IStorageRepository storageRepository,
        IEventProcessor eventProcessor,
        INewIdGenerator idGenerator)
    {
        _repository = repository;
        _uomRepository = uomRepository;
        _productAvailabilityRepository = productAvailabilityRepository;
        _storageRepository = storageRepository; 
        _eventProcessor = eventProcessor;
        _idGenerator = idGenerator;
    }


    public async Task HandleAsync(AddProduct command, CancellationToken cancellationToken = new CancellationToken())
    {
        await Validate(command);

        var mainUnitOfMeasure = await _uomRepository.GetAsync(command.MainUnitOfMeasureId);
        var product = Product.Create(command.ProductId, command.ProductName, mainUnitOfMeasure!);

        if (command.UnitOfMeasures.Any())
        {
            var unitOfMeasures = await _uomRepository.GetListByIdsAsync(command.UnitOfMeasures);
            product.AddUnitOfMeasure(unitOfMeasures);
        }

        
        //TODO: open transaction (desirable have outbox pattern
        await _repository.AddAsync(product);


        if (command.AddInitialQuantity)
        {
            var storage = await _storageRepository.GetAsync(command.StorageId!.Value);
            UnitOfMeasure? unitOfMeasure = product.MainUnitOfMeasure;

            if (command.AmountInUnitOfMeasureId.HasValue)
            {
                unitOfMeasure = await _uomRepository.GetAsync(command.AmountInUnitOfMeasureId.Value);
            }

            var initialQuantity = new QuantityOfProduct(product, unitOfMeasure!, command.InitialAmount);
            var productAvailability = new ProductAvailability(_idGenerator.GetNewIdFor(nameof(ProductAvailability)), initialQuantity, storage!);
            await _productAvailabilityRepository.AddAsync(productAvailability);
        }

        await _eventProcessor.ProcessAsync(product.Events);

    }

    public async Task Validate(AddProduct command)
    {
        if (!await _repository.ExistsAsync(command.ProductId))
        {
            throw new ProductAlreadyExistsException(command.ProductId);
        }

        if (!await _repository.ExistsAsync(command.ProductName))
        {
            throw new ProductAlreadyExistsException(command.ProductId);
        }

        if (!await _uomRepository.ExistsAsync(command.MainUnitOfMeasureId))
        {
            throw new UnitOfMeasureNotFoundException(command.MainUnitOfMeasureId);
        }

        if (command.UnitOfMeasures.Any())
        {
            if (!await _uomRepository.ExistsAsync(command.UnitOfMeasures))
            {
                throw new UnitOfMeasureNotFoundException(command.UnitOfMeasures);
            }
        }

        if (command.AddInitialQuantity)
        {
            if (command.StorageId.HasValue)
            {
                if (!await _storageRepository.ExistsAsync(command.StorageId.Value))
                {
                    throw new StorageNotFoundException(command.StorageId.Value);
                }
            }
            else
            {
                throw new InitialProductAvailabilityCantBeAddedException();
            }
        
            if (command.AmountInUnitOfMeasureId.HasValue)
            {
                if (!await _uomRepository.ExistsAsync(command.AmountInUnitOfMeasureId.Value))
                {
                    throw new UnitOfMeasureNotFoundException(command.AmountInUnitOfMeasureId.Value);
                }
            }
        }
    }
}