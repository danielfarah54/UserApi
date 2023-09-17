using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using UserApi.Models;

namespace UserApi.Services;

public class TokenService
{
  private readonly IConfiguration _configuration;
  public TokenService(IConfiguration configuration)
  {
    _configuration = configuration;
  }

  public string GenerateToken(User user)
  {
    Claim[] claims = new[]
    {
      new Claim(ClaimTypes.NameIdentifier, user.Id),
      new Claim(ClaimTypes.Name, user.UserName!),
      new Claim(ClaimTypes.DateOfBirth, user.BirthDate.ToString()),
      new Claim(ClaimTypes.AuthenticationInstant, DateTime.UtcNow.ToString()),
    };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SymmetricSecurityKey"]!));

    var token = new JwtSecurityToken(
      claims: claims,
      expires: DateTime.UtcNow.AddDays(7),
      signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
  }
}