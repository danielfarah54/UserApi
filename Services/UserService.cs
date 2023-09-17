using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UserApi.Data.Dtos;
using UserApi.Models;

namespace UserApi.Services;

public class UserService
{

  private readonly IMapper _mapper;
  private readonly UserManager<User> _userManager;
  private readonly SignInManager<User> _signInManager;
  private readonly TokenService _tokenService;

  public UserService(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, TokenService tokenService)
  {
    _mapper = mapper;
    _userManager = userManager;
    _signInManager = signInManager;
    _tokenService = tokenService;
  }
  public async Task<string> Create(CreateUserDto userDto)
  {
    User user = _mapper.Map<User>(userDto);
    IdentityResult result = await _userManager.CreateAsync(user, userDto.Password);

    if (!result.Succeeded)
    {
      throw new ApplicationException("User creation failed");
    }

    return user.Id;
  }

  public async Task<string> Login(LoginDto loginDto)
  {
    var result = await _signInManager.PasswordSignInAsync(loginDto.Username, loginDto.Password, false, false);

    if (!result.Succeeded)
    {
      throw new ApplicationException("Login failed");
    }

    User user = _signInManager.UserManager.Users.FirstOrDefault(u => u.NormalizedUserName == loginDto.Username.ToUpper())!;

    return _tokenService.GenerateToken(user);
  }
}