namespace FoodRocket.Services.Identity.Core.Entities
{
    public static class Role
    {
        public const string User = "user";
        public const string Admin = "admin";
        public const string Waiter = "waiter";
        public const string Manager = "manager";
        public static bool IsValid(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
            {
                return false;
            }

            role = role.ToLowerInvariant();

            return role == User || role == Admin || role == Waiter || role == Manager;
        }
    }
}
