using SSW.CleanArchitecture.Application.Common.Interfaces;

namespace SSW.CleanArchitecture.Database;

public class MockCurrentUserService : ICurrentUserService
{
    public string UserId => "00000000-0000-0000-0000-000000000000";
}