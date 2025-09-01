using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration; 
using Microsoft.AspNetCore.Authorization; 

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("register")] // /api/Auth/register
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        // Burada gerçek kullanıcı kayıt mantığınızı uygulayın.
        // Örneğin: Veritabanına kullanıcıyı kaydetme, şifreyi hashleme vb.
        // Basit bir örnek olduğu için sadece mesaj dönüyoruz.
        // Gerçek projede kullanıcı kontrolü, şifre hashleme vb. mutlaka olmalı.
        return Ok(new { Message = "Kullanıcı başarıyla kaydedildi." });
    }

    [HttpPost("login")] // /api/Auth/login
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        // Gerçek projede veritabanınızdan kullanıcı adı ve şifreyi doğrulayın.
        // Şifreler asla düz metin olarak karşılaştırılmamalı, hashlenmiş şifreler kullanılmalıdır.
        if (model.Username == "testuser" && model.Password == "password") // SADECE ÖRNEK KONTROL!
        {
            var token = GenerateJwtToken(model.Username);
            return Ok(new { Token = token });
        }

        return Unauthorized(new { Message = "Kullanıcı adı veya şifre yanlış." });
    }

    private string GenerateJwtToken(string username)
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

        // Token içine eklemek istediğiniz bilgileri (claim'ler) buraya yazın.
        // Örneğin kullanıcı ID'si, rolleri vb.
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username), // Kullanıcı adı claim'i
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Benzersiz token ID'si
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpiryMinutes"])),
            Issuer = jwtSettings["Issuer"],
            Audience = jwtSettings["Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    // Bu endpoint'e sadece geçerli bir JWT tokenı ile erişilebilir.
    [HttpGet("securedata")] // /api/Auth/securedata
    [Authorize] // Bu attribute sayesinde sadece yetkili kullanıcılar erişebilir
    public IActionResult GetSecureData()
    {
        // Token'dan kullanıcı adını alma örneği
        var username = User.Identity.Name;
        return Ok(new { Data = $"Merhaba {username}, bu gizli bir veridir!" });
    }
}

// Model tanımları (aynı dosyada veya ayrı bir klasörde olabilir)
public class RegisterModel
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}