using ChineseAction.Api.Model;
using ChineseAction.Api.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PurchaserController : ControllerBase
{
    private readonly IPurchaserService _purchaserService;

    public PurchaserController(IPurchaserService purchaserService)
    {
        _purchaserService = purchaserService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<Purchaser>> Register([FromBody] Purchaser purchaser)
    {
        var registeredPurchaser = await _purchaserService.RegisterAsync(purchaser);
        return CreatedAtAction(nameof(Register), new { id = registeredPurchaser.Id }, registeredPurchaser);
    }

    [HttpPost("login")]
    public async Task<ActionResult<Purchaser>> Login([FromBody] LoginDto loginDto)
    {
        var purchaser = await _purchaserService.LoginAsync(loginDto.Email, loginDto.Password);
        if (purchaser == null)
        {
            return Unauthorized("Invalid email or password.");
        }
        return Ok(purchaser);
    }
}