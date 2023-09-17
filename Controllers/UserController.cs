using Microsoft.AspNetCore.Mvc;
using UserApi.Data.Dtos;
using UserApi.Services;

namespace UserApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
  private readonly UserService _userService;
  public UserController(UserService userService)
  {
    _userService = userService;
  }

  [HttpPost("create")]
  public async Task<IActionResult> CreateUser(CreateUserDto userDto)
  {
    string userId = await _userService.Create(userDto);

    return Ok("User created successfully");
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login(LoginDto loginDto)
  {
    var token = await _userService.Login(loginDto);

    return Ok(token);
  }
}