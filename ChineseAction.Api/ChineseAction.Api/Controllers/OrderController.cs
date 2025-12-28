using ChineseAction.Api.DTOs;
using ChineseAction.Api.Model;
using ChineseAction.Api.Servies;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly ILogger<OrderController> _logger; // 1. משתנה ללוגר

    public OrderController(IOrderService orderService, ILogger<OrderController> logger)
    {
        _orderService = orderService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetDraftOrderByPurchaserIdAsync(int purchaserID)
    {
        var orders = await _orderService.GetDraftOrderByPurchaserIdAsync(purchaserID);
        return Ok(orders);
    }

    /// <summary>
    /// הוספה לסל (Add to Cart)
    /// בדיקה: האם יש כבר הזמנה טיוטה (IsDraft=true) עבור Purchaser.
    /// אם לא קיימת — יוצרים הזמנה טיוטה חדשה.
    /// לאחר מכן מוסיפים/מעדכנים שורת OrderItem.
    /// </summary>
    [HttpPost("add-to-cart")]
    public async Task<ActionResult<Order>> AddToCart([FromBody] AddToCartDto dto)
    {
        try
        {
            _logger.LogInformation("User {UserId} is trying to add Gift {GiftId} to cart.", dto.PurchaserId, dto.GiftId);
            var order = await _orderService.AddToCartAsync(dto);
            _logger.LogInformation("Successfully added item to cart for Order ID: {OrderId}", order.Id);
            return Ok(order);
        }
        catch (ArgumentException aex)
        {
            return BadRequest(aex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while adding to cart for User {UserId}", dto.PurchaserId);
            return StatusCode(500, new { message = ex.Message, stackTrace = ex.StackTrace });
        }
    }
    // סיום הזמנה

    [HttpPost("complete-order")]
    public async Task<ActionResult<Order>> CompleteOrder([FromBody] CompleteOrderDto dto)
    {
        try
        {
            _logger.LogInformation("User {UserId} is trying to complete Order {OrderId}.", dto.PurchaserId, dto.OrderId);
            var order = await _orderService.CompleteOrderAsync(dto);
            _logger.LogInformation("Successfully completed Order ID: {OrderId}", order.Id);
            return Ok(order);
        }
        catch (ArgumentException aex)
        {
            return BadRequest(aex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while completing Order {OrderId} for User {UserId}", dto.OrderId, dto.PurchaserId);
            return StatusCode(500, new { message = ex.Message, stackTrace = ex.StackTrace });
        }
    }

}

