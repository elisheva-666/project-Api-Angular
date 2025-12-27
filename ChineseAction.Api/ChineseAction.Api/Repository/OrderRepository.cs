using ChineseAction.Api.Data;
using ChineseAction.Api.Model;
using Microsoft.EntityFrameworkCore;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // מחזיר הזמנה טיוטה (IsDraft = true) עבור Purchaser מסוים, כולל הפריטים
    public async Task<Order?> GetDraftOrderByPurchaserIdAsync(int purchaserId)
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.PurchaserId == purchaserId && o.IsDraft);
    }

    // יוצר הזמנה חדשה (טיוטה)
    public async Task<Order> CreateDraftOrderAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }

    // מחזיר שורת OrderItem קיימת עבור הזמנה ומתנה ספציפית
    public async Task<OrderItem?> GetOrderItemAsync(int orderId, int giftId)
    {
        return await _context.OrderItems
            .FirstOrDefaultAsync(oi => oi.OrderId == orderId && oi.GiftId == giftId);
    }

    // מוסיף שורת OrderItem חדשה
    public async Task<OrderItem> AddOrderItemAsync(OrderItem item)
    {
        _context.OrderItems.Add(item);
        await _context.SaveChangesAsync();
        return item;
    }

    // מעדכן שורת OrderItem כשהכמות משתנה)
    public async Task<OrderItem> UpdateOrderItemAsync(OrderItem item)
    {
        _context.OrderItems.Update(item);
        await _context.SaveChangesAsync();
        return item;
    }

    //קבלת הזמנה לפי מזהה
    public async Task<Order?> GetOrderByIdAsync(int orderId)
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == orderId);
    }
}