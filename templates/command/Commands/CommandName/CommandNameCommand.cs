using SSW.CleanArchitecture.Application.Common.Interfaces;

namespace SSW.CleanArchitecture.Application.UseCases.EntityNames.Commands.CommandName;

public record CommandNameCommand() : IRequest<ErrorOr<Success>>;

public class CommandNameCommandHandler : IRequestHandler<CommandNameCommand, ErrorOr<Success>>
{
    private readonly IApplicationDbContext _dbContext;

    public CommandNameCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<Success>> Handle(CommandNameCommand request, CancellationToken cancellationToken)
    {
        // TODO: Add your business logic and persistence here

        throw new NotImplementedException();
    }
}