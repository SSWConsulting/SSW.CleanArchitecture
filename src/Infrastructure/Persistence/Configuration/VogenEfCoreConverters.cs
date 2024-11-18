using SSW.CleanArchitecture.Domain.Heroes;
using SSW.CleanArchitecture.Domain.Teams;
using Vogen;

namespace SSW.CleanArchitecture.Infrastructure.Persistence.Configuration;

[EfCoreConverter<TeamId>]
[EfCoreConverter<HeroId>]
[EfCoreConverter<MissionId>]
internal sealed partial class VogenEfCoreConverters;