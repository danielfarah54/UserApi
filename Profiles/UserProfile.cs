namespace UserApi.Profiles;

using AutoMapper;
using UserApi.Data.Dtos;
using UserApi.Models;

public class UserProfile : Profile
{
  public UserProfile()
  {
    CreateMap<CreateUserDto, User>();
  }
}