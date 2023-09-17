using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace UserApi.Authorization;

public class AgeAuthorization : AuthorizationHandler<MinimumAgeRequirement>
{
  protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
  {
    var dateOfBirthClaim = context.User.FindFirstValue(ClaimTypes.DateOfBirth);

    if (dateOfBirthClaim is null)
    {
      context.Fail();
      return Task.CompletedTask;
    }

    var dateOfBirth = DateTime.Parse(dateOfBirthClaim);

    var age = DateTime.Today.Year - dateOfBirth.Year;
    if (dateOfBirth.Date > DateTime.Today.AddYears(-age))
    {
      age--;
    }

    if (age >= requirement.MinimumAge)
    {
      context.Succeed(requirement);
    }

    return Task.CompletedTask;
  }
}