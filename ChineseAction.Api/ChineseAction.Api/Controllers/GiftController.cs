using ChineseAction.Api.NewFolder;
using ChineseAction.Api.Servies;
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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Gift>>> GetAllGift()
    {
        var gifts = await _giftService.GetAllGift();
        return Ok(gifts);
    }
}
