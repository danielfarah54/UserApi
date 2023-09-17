using Microsoft.AspNetCore.Authorization;

namespace UserApi.Authorization;

public class MinimumAgeRequirement : IAuthorizationRequirement
{
  public int MinimumAge { get; }

  public MinimumAgeRequirement(int minimumAge)
  {
    MinimumAge = minimumAge;
  }
}