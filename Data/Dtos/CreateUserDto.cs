using System.ComponentModel.DataAnnotations;

namespace UserApi.Data.Dtos;

public class CreateUserDto
{
  [Required]
  public string Username { get; set; } = default!;

  [Required]
  public DateTime BirthDate { get; set; } = default!;

  [Required]
  [DataType(DataType.Password)]
  public string Password { get; set; } = default!;

  [Required]
  [Compare("Password")]
  public string PasswordConfirmation { get; set; } = default!;
}