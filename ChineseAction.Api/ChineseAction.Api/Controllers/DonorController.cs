using ChineseAction.Api.Model;
using ChineseAction.Api.Servies;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class DonorController : ControllerBase
{
    private readonly IDonorService _donorService;

    public DonorController(IDonorService donorService)
    {
        _donorService = donorService;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<Donor>>> GetAllDonors()
    {
        var donors = await _donorService.GetAllDonorsAsync();
        return Ok(donors);
    }

    [HttpGet("filter")]
    public async Task<ActionResult<IEnumerable<Donor>>> GetFilteredDonors([FromQuery] string? name, [FromQuery] string? email)
    {
        // בדיקה אם שני הפרמטרים נשלחו יחד
        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(email))
        {
            return BadRequest("Cannot filter by both name and email simultaneously.");
        }

        // קריאה לשירות לקבלת רשימת תורמים מסוננת
        var donors = await _donorService.GetFilteredDonorsAsync(name, email);
        return Ok(donors);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDonor(int id)
    {
        var success = await _donorService.DeleteDonorAsync(id);
        if (!success)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDonor(int id, [FromBody] Donor donor)
    {
        if (id != donor.Id)
        {
            return BadRequest("ID mismatch.");
        }

        var updatedDonor = await _donorService.UpdateDonorAsync(donor);
        if (updatedDonor == null)
        {
            return NotFound();
        }
        return Ok(updatedDonor);
    }

    [HttpPost]
    public async Task<ActionResult<Donor>> AddDonor([FromBody] Donor donor)
    {
        var addedDonor = await _donorService.AddDonorAsync(donor);
        return CreatedAtAction(nameof(GetAllDonors), new { id = addedDonor.Id }, addedDonor);
    }
}