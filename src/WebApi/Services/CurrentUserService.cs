using SSW.CleanArchitecture.Application.Common.Interfaces;
using System.Security.Claims;

namespace SSW.CleanArchitecture.WebApi.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public string? UserId => httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
}