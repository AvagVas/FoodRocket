namespace FoodRocket.DBContext.Services.SettingOptions;

public class IDGeneratorConfigurationOptions
{
    public bool Enabled { get; set; }
    public IDStructureBits? StructureBits { get; set; }

    public GeneratorEpoch? Epoch { get; set; }

    public IEnumerable<Generator> Generators { get; set; }

    public class IDStructureBits
    {
        public byte TimestampBits { get; set; }
        public byte GeneratorBits { get; set; }
        public byte SequenceBits { get; set; }
    }

    public class GeneratorEpoch
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }

    }

    // {
    //     "name": "user",
    //     "id": 1,
    //     "shouldAddIdPrefix": true
    // }
    public class Generator
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public bool ShouldAddIdPrefix { get; set; }
    }
}