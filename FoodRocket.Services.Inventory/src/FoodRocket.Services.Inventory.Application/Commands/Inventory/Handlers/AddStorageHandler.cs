using Convey.CQRS.Commands;
using FoodRocket.Services.Inventory.Application.Exceptions;
using FoodRocket.Services.Inventory.Core.Entities.Inventory;
using FoodRocket.Services.Inventory.Core.Repositories;

namespace FoodRocket.Services.Inventory.Application.Commands.Inventory.Handlers;

public class AddStorageHandler : ICommandHandler<AddStorage>
{
    private readonly IStorageRepository _repository;

    public AddStorageHandler(IStorageRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(AddStorage command, CancellationToken cancellationToken = new CancellationToken())
    {
        var storage = new Storage(command.StorageId, command.StorageName, command.ManagerId);
        if (await _repository.ExistsAsync(storage.Id))
        {
            throw new StorageAlreadyExistsException(storage.Id, storage.Name);
        }

        await _repository.AddAsync(storage);
    }
}