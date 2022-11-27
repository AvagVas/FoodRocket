using Convey.CQRS.Commands;
using FoodRocket.Services.Inventory.Application.Exceptions;
using FoodRocket.Services.Inventory.Core.Entities.Inventory;
using FoodRocket.Services.Inventory.Core.Repositories;

namespace FoodRocket.Services.Inventory.Application.Commands.Inventory.Handlers;

public class UpdateStorageHandler : ICommandHandler<UpdateStorage>
{
    private readonly IStorageRepository _storageRepository;

    public UpdateStorageHandler(IStorageRepository storageRepository)
    {
        _storageRepository = storageRepository;
    }

    public async Task HandleAsync(UpdateStorage command, CancellationToken cancellationToken = new CancellationToken())
    {
        var storage = await _storageRepository.GetAsync(command.StorageId);
        if (storage is null)
        {
            throw new StorageNotFoundException(command.StorageId);
        }

        if (!string.IsNullOrWhiteSpace(command.StorageName))
        {
            storage.ChangeName(command.StorageName);
        }

        if (command.ManagerId > 0)
        {
            storage.ChangeManager(command.ManagerId);
        }

        await _storageRepository.UpdateAsync(storage);
    }
}