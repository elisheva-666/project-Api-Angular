using ChineseAction.Api.Model;
using ChineseAction.Api.Repository;
using ChineseAction.Api.DTOs;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    

    // הוספה לסל: מוצא טיוטה קיימת או יוצר חדשה, ואז מוסיף/מעדכן שורת OrderItem.
    public async Task<Order> AddToCartAsync(AddToCartDto dto)
    {
        if (dto.Quantity <= 0)
        {
            throw new ArgumentException("Quantity must be at least 1.");
        }

        try
        {
            // בדיקה אם קיימת הזמנה טיוטה עבור הרוכש
            var draft = await _orderRepository.GetDraftOrderByPurchaserIdAsync(dto.PurchaserId);

            if (draft == null)
            {
                // יצירת הזמנה טיוטה חדשה
                draft = new Order
                {
                    PurchaserId = dto.PurchaserId,
                    OrderDate = DateTime.UtcNow,
                    IsDraft = true
                };

                draft = await _orderRepository.CreateDraftOrderAsync(draft);
            }

            // בדיקה אם כבר קיימת שורת פריט זהה
            var existingItem = await _orderRepository.GetOrderItemAsync(draft.Id, dto.GiftId);

            if (existingItem != null)
            {
                // עדכון כמות
                existingItem.Quantity += dto.Quantity;
                await _orderRepository.UpdateOrderItemAsync(existingItem);
            }
            else
            {
                // הוספת שורת פריט חדשה
                var newItem = new OrderItem
                {
                    OrderId = draft.Id,
                    GiftId = dto.GiftId,
                    Quantity = dto.Quantity
                };
                await _orderRepository.AddOrderItemAsync(newItem);
            }

            // נחזיר את ההזמנה (טעינה מחדש אם רוצים לכלול פריטים מעודכנים)
            var updatedDraft = await _orderRepository.GetDraftOrderByPurchaserIdAsync(dto.PurchaserId);
            return updatedDraft!;
        }
        catch (Exception)
        {
            // אפשר להוסיף לוג כאן אם יש ILogger
            throw;
        }

        
    }
    // קבלת הזמנה לפי id של קונה
    public async Task<Order?> GetDraftOrderByPurchaserIdAsync(int purchaserId)
    {
        return await _orderRepository.GetDraftOrderByPurchaserIdAsync(purchaserId);
    }

    public async Task<Order> CompleteOrderAsync(CompleteOrderDto dto)
    {
        var order = await _orderRepository.GetOrderByIdAsync(dto.OrderId);
        if (order == null || order.PurchaserId != dto.PurchaserId || !order.IsDraft)
        {
            throw new ArgumentException("Invalid order for completion.");
        }
        // סימון ההזמנה כלא טיוטה
        order.IsDraft = false;
        // ניתן להוסיף כאן לוגיקה נוספת לסיום ההזמנה (תשלום, עדכון מלאי וכו')
        // שמירת השינויים
        await _orderRepository.UpdateOrderAsync(order);
        return order;
    }
}