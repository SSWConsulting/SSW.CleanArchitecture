namespace Application.Common.Interfaces;

public interface IDateTime
{
    // TODO: Talk to Gordon about this - System Clock
    public DateTimeOffset Now => DateTimeOffset.UtcNow;
}