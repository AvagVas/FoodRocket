using Convey.CQRS.Commands;
using FoodRocket.Services.Inventory.Application.Exceptions;
using FoodRocket.Services.Inventory.Application.Services;
using FoodRocket.Services.Inventory.Core.Entities.Inventory;
using FoodRocket.Services.Inventory.Core.Repositories;
using FoodRocket.Services.Inventory.Core.ValueObjects;

namespace FoodRocket.Services.Inventory.Application.Commands.Inventory.Handlers;

public class AddUnitOfMeasureHandler : ICommandHandler<AddUnitOfMeasure>
{
    private readonly IUnitOfMeasureRepository _uomRepository;
    private readonly INewIdGenerator _idGenerator;


    public AddUnitOfMeasureHandler(IUnitOfMeasureRepository uomRepository, INewIdGenerator idGenerator)
    {
        _uomRepository = uomRepository;
        _idGenerator = idGenerator;
    }


    public async Task HandleAsync(AddUnitOfMeasure command, CancellationToken cancellationToken = new CancellationToken())
    {
        if (await _uomRepository.ExistsByNameOrIdAsync(command.Name, command.UnitOfMeasureId))
        {
            throw new UnitOfMeasureAlreadyExistsException(command.UnitOfMeasureId);
        }

        UnitOfMeasure? baseOfUnitOfM = null;
        if (!command.IsBase)
        {
            baseOfUnitOfM = await _uomRepository.GetAsync(command.BaseUnitOfMeasureId);

            if (baseOfUnitOfM is null)
            {
                throw new UnitOfMeasureNotFoundException(command.BaseUnitOfMeasureId);
            }
        }

        if (!Enum.TryParse(command.TypeOfUnitOfMeasure, out TypeOfUnitOfMeasure parsedTypeOfUoM))
        {
            throw new InvalidNameOfTypeForUnitOfMeasureException(command.TypeOfUnitOfMeasure);
        }

        UnitOfMeasure newUnitOfMeasure = new(
            _idGenerator.GetNewIdFor(nameof(UnitOfMeasure)),
            parsedTypeOfUoM,
            command.Name,
            command.IsBase,
            baseOfUnitOfM,
            command.Ratio,
            command.IsFractional);

        await _uomRepository.AddAsync(newUnitOfMeasure);
    }
}