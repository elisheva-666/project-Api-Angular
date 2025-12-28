using ChineseAction.Api.Data;
using ChineseAction.Api.DTOs;
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

    public async Task UpdateOrderAsync(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }
    public async Task<OrderItem> UpdateOrderItemAsync(int orderItemId, int newQuantity)
    {
        var item = await _context.OrderItems.FindAsync(orderItemId);
        if (item == null)
        {
            throw new ArgumentException("Order item not found.");
        }
        item.Quantity = newQuantity;
        _context.OrderItems.Update(item);
        await _context.SaveChangesAsync();
        return item;
    }
}