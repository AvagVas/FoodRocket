namespace FoodRocket.DBContext.Models.Orders;

public enum OrderStatus
{
    New = 1,
    Approved = 2,
    ReadyToDeliver = 3,
    OnRoad = 4,
    Delivered = 5,
    Payed = 6,
    Cancelled = 7
}