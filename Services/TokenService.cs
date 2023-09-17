using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using UserApi.Models;

namespace UserApi.Services;

public class TokenService
{
  public string GenerateToken(User user)
  {
    Claim[] claims = new[]
    {
      new Claim(ClaimTypes.NameIdentifier, user.Id),
      new Claim(ClaimTypes.Name, user.UserName!),
      new Claim(ClaimTypes.AuthenticationInstant, DateTime.UtcNow.ToString()),
    };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("lkajsdoij20198ejiwq0de9ei2ojen2-0eioiqpwoej02"));

    var token = new JwtSecurityToken(
      claims: claims,
      expires: DateTime.UtcNow.AddDays(7),
      signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
  }
}