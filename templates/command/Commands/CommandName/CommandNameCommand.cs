using SSW.CleanArchitecture.Application.Common.Interfaces;

namespace SSW.CleanArchitecture.Application.UseCases.EntityNames.Commands.CommandName;

public record CommandNameCommand() : IRequest<ErrorOr<Success>>;

// dbContext is injected ready for the persistence you are about to write. Delete the pragmas once Handle reads it.
#pragma warning disable CS9113 // Parameter is unread
internal sealed class CommandNameCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<CommandNameCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(CommandNameCommand request, CancellationToken cancellationToken)
    {
        // TODO: Add your business logic and persistence here

        throw new NotImplementedException();
    }
}
#pragma warning restore CS9113

internal sealed class CommandNameCommandValidator : AbstractValidator<CommandNameCommand>
{
    public CommandNameCommandValidator()
    {
        // TODO: Add your validation rules here.  For example: RuleFor(p => p.Foo).NotEmpty()
    }
}