using FoodRocket.Services.Identity.Application.Services;
using FoodRocket.Services.Identity.Infrastructure.Exceptions;
using FoodRocket.Services.Identity.Infrastructure.SettingOptions;
using IdGen;
using Microsoft.IdentityModel.Tokens;

namespace FoodRocket.Services.Identity.Infrastructure.Services;

public class NewIdGenerator : INewIdGenerator
{
    private readonly IDGeneratorConfigurationOptions _configOptions;
    private readonly IdStructure _structure;
    private readonly DateTime _epoch;
    private readonly IdGeneratorOptions _options;
    private readonly Dictionary<string, IdGenerator> generators= new ();
    private readonly Dictionary<string, int> generatorIdMappingToName = new ();
    
    public NewIdGenerator(IDGeneratorConfigurationOptions configOptions)
    {
        _configOptions = configOptions;

        if (configOptions.StructureBits is null)
        {
            throw new IdGeneratorMisconfiguredException();
        }

        _structure = new IdStructure(
            configOptions.StructureBits.TimestampBits, 
            configOptions.StructureBits.GeneratorBits, 
            configOptions.StructureBits.SequenceBits);

        if (configOptions.Epoch is null)
        {
            throw new IdGeneratorMisconfiguredException();
        }

        _epoch = new DateTime(
            configOptions.Epoch.Year, 
            configOptions.Epoch.Month, 
            configOptions.Epoch.Day, 
            configOptions.Epoch.Hour, 
            configOptions.Epoch.Minute, 
            configOptions.Epoch.Second, 
            DateTimeKind.Utc);
        
        _options = new IdGeneratorOptions(_structure, new DefaultTimeSource(_epoch));

        InstantiateGenerators();
    }

    public void InstantiateGenerators()
    {
        var idPrefixText = Environment.GetEnvironmentVariable("ID_GENERATOR_PREFIX");

        int generatorIdPrefix = 0;
        if (!string.IsNullOrWhiteSpace(idPrefixText) && int.TryParse(idPrefixText, out int parsedValue))
        {
            generatorIdPrefix = parsedValue;
        }

        if (_configOptions.Generators is null)
        {
            throw new IdGeneratorMisconfiguredException();
        }
        
        foreach (var generator in _configOptions.Generators)
        {
            int generatorId = generator.Id;
            if (generator.ShouldAddIdPrefix)
            {
                generatorId += generatorIdPrefix;
            }

            if (!string.IsNullOrWhiteSpace(generator.Name))
            {
                var generatorInstance = new IdGenerator(generatorId, _options);
                
                generators.Add(generator.Name, generatorInstance);
            }

        }
    }

    public long GetNewIdFor(string generatorName)
    {
        if (!generators.ContainsKey(generatorName))
        {
            throw new IdGeneratorMisconfiguredException();
        }

        var generator = generators[generatorName];
        return generator.CreateId();
    }
}
