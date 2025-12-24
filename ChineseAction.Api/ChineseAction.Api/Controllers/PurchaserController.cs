using ChineseAction.Api.Model;
using ChineseAction.Api.Servies;
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

    // צפייה ברשימת הרוכשים
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<Purchaser>>> GetAllPurchasers()
    {
        var purchasers = await _purchaserService.GetAllPurchasersAsync();
        return Ok(purchasers);
    }

    // צפייה ברוכש ספציפי לפי ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Purchaser>> GetPurchaserById(int id)
    {
        var purchaser = await _purchaserService.GetPurchaserByIdAsync(id);
        if (purchaser == null)
        {
            return NotFound();
        }
        return Ok(purchaser);
    }
}