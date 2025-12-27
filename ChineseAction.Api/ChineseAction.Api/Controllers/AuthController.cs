using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    // כאן היית מזריק את ה-DbContext שלך

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto request)
    {
        // 1. בדיקה מול ה-DB (דוגמה סטטית למהירות)
        // בפועל: חפש בטבלת משתמשים/מנהלים לפי שם וסיסמה
        if (request.Email != "e0548571666@gmail.com" || request.Password != "123456")
        {
            return Unauthorized("שם משתמש או סיסמה שגויים");
        }

        // 2. יצירת הטוקן
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, request.Email),
            new Claim(ClaimTypes.Role, "manager"), // חשוב מאוד לדרישה מס' 13!
            new Claim("UserId", "1") // ה-ID של המנהל
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: creds
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(new { token = jwt });
    }
}