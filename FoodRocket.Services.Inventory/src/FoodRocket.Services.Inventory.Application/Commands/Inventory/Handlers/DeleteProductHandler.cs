using Convey.CQRS.Commands;
using FoodRocket.Services.Inventory.Application.Exceptions;
using FoodRocket.Services.Inventory.Application.Services;
using FoodRocket.Services.Inventory.Core.Entities.Inventory;
using FoodRocket.Services.Inventory.Core.Repositories;
using FoodRocket.Services.Inventory.Core.ValueObjects;

namespace FoodRocket.Services.Inventory.Application.Commands.Inventory.Handlers;

public class DeleteProductHandler : ICommandHandler<DeleteProduct>
{
    private readonly IProductRepository _repository;
    private readonly IEventProcessor _eventProcessor;


    public DeleteProductHandler(IProductRepository repository,
        IEventProcessor eventProcessor
    )
    {
        _repository = repository;
        _eventProcessor = eventProcessor;
    }


    public async Task HandleAsync(DeleteProduct command, CancellationToken cancellationToken = new CancellationToken())
    {
        var product = await _repository.GetAsync(command.ProductId);
        if (product is null)
        {
            throw new ProductNotFoundException(command.ProductId);
        }

        //TODO: add some policies to prevent deletion
        product.Delete();
        await _eventProcessor.ProcessAsync(product!.Events);
    }
}