namespace FoodRocket.Services.Tornado.Infrastructure.SettingOptions;

public class EmailClientConfigurationOptions
{
    public bool Enabled { get; set; }
    
    public string From { get; set; }

    public string EmailItemsPath { get; set; } =
        Environment.GetEnvironmentVariable("FOOD_ROCKET_EMAIL_ITEMS_PICKUP_PATH");
    
    public string SmtpClientHost { get; set; }
}