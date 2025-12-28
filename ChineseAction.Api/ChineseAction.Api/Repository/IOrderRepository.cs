using ChineseAction.Api.DTOs;
using ChineseAction.Api.Model;

public interface IOrderRepository
{
    Task<Order?> GetDraftOrderByPurchaserIdAsync(int purchaserId);
    Task<Order> CreateDraftOrderAsync(Order order);
    Task<OrderItem?> GetOrderItemAsync(int orderId, int giftId);
    Task<OrderItem> AddOrderItemAsync(OrderItem item);
    Task<OrderItem> UpdateOrderItemAsync(OrderItem item);
    ////GetOrderByIdAsync
    //Task<OrderItem> UpdateOrderItemAsync(int orderItemId, int newQuantity);
    //Task<Order> GetOrderByIdAsync(int orderId);
    ////UpdateOrderAsync
    //Task<Order> GetOrderByIdAsync(Order order);
    Task  UpdateOrderAsync(Order item);
    //var order = await _orderRepository.GetOrderByIdAsync(dto.OrderId);
    Task<Order?> GetOrderByIdAsync(int orderId);

    // await _orderRepository.UpdateOrderAsync(order);




}