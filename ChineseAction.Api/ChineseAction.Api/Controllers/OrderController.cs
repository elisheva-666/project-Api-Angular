using ChineseAction.Api.DTOs;
using ChineseAction.Api.Model;
using ChineseAction.Api.Servies;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
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
            var order = await _orderService.AddToCartAsync(dto);
            return Ok(order);
        }
        catch (ArgumentException aex)
        {
            return BadRequest(aex.Message);
        }
        catch (Exception ex)
        {
            // ניתן לשפר ולהחזיר מידע מפורט יותר או לוג
            return StatusCode(500, new { message = ex.Message, stackTrace = ex.StackTrace });
        }
    }
}

