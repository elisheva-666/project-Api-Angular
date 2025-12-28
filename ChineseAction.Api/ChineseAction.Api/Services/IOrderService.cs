using ChineseAction.Api.Model;
using ChineseAction.Api.DTOs;

public interface IOrderService
{
    // בונה/מעדכן טיוטת הזמנה והוספת פריט - מחזיר ההזמנה (כולל פריטים)
    Task<Order> AddToCartAsync(AddToCartDto dto);

    Task<Order?> GetDraftOrderByPurchaserIdAsync(int purchaserId);
    Task<Order> CompleteOrderAsync(CompleteOrderDto dto);
}