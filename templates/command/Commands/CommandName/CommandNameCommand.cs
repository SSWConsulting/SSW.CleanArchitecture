using SSW.CleanArchitecture.Application.Common.Interfaces;

namespace SSW.CleanArchitecture.Application.UseCases.EntityNames.Commands.CommandName;

public record CommandNameCommand() : IRequest<ErrorOr<Success>>;

internal sealed class CommandNameCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<CommandNameCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(CommandNameCommand request, CancellationToken cancellationToken)
    {
        // TODO: Add your business logic and persistence here

        throw new NotImplementedException();
    }
}

internal sealed class CommandNameCommandValidator : AbstractValidator<CommandNameCommand>
{
    public CommandNameCommandValidator()
    {
        // TODO: Add your validation rules here.  For example: RuleFor(p => p.Foo).NotEmpty()
    }
}