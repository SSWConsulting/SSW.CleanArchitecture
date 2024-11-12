using SSW.CleanArchitecture.WebApi;
using System.Reflection;

namespace SSW.CleanArchitecture.Architecture.UnitTests.Common;

public abstract class BaseTest
{
    protected static readonly Assembly DomainAssembly = typeof(Domain.Common.Base.AggregateRoot<>).Assembly;
    protected static readonly Assembly ApplicationAssembly = typeof(Application.Common.Interfaces.IApplicationDbContext).Assembly;
    protected static readonly Assembly InfrastructureAssembly = typeof(Infrastructure.Persistence.ApplicationDbContext).Assembly;
    protected static readonly Assembly WebApiAssembly = typeof(IWebApiMarker).Assembly;
}