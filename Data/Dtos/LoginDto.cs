using System.ComponentModel.DataAnnotations;

namespace UserApi.Data.Dtos;

public class LoginDto
{
  [Required]
  public string Username { get; set; } = default!;

  [Required]
  [DataType(DataType.Password)]
  public string Password { get; set; } = default!;
}