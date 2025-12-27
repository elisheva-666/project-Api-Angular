using ChineseAction.Api.Model;
using ChineseAction.Api.Servies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class GiftController : ControllerBase
{
    private readonly IGiftService _giftService;

    public GiftController(IGiftService service)
    {
        _giftService = service;
    }
    //מחזיר את כל המתנות
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Gift>>> GetAllGift()
    {
        var gifts = await _giftService.GetAllGift();
        return Ok(gifts);
    }

    [HttpGet("sorted")]
    public async Task<ActionResult<IEnumerable<Gift>>> GetSortedGifts([FromQuery] string? sortBy)
    {
        // קריאה לשירות לקבלת רשימת מתנות ממוין לפי הפרמטר שנשלח
        var gifts = await _giftService.GetSortedGiftsAsync(sortBy);
        return Ok(gifts);
    }

    [Authorize(Roles = "manager")]
    //מחיקת מתנה תורם  
    [HttpDelete]
    public async Task<ActionResult> DeleteGift(int id)
    {
        var result = await _giftService.DeleteGift(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
    [Authorize(Roles = "manager")]
    //הוספת מתנה לתורם
    [HttpPost]
    public async Task<ActionResult<Gift>> AddGift(Gift gift)
    {
        var createdGift = await _giftService.AddGift(gift);
        return CreatedAtAction(nameof(GetAllGift), new { id = createdGift.Id }, createdGift);
    }
}
