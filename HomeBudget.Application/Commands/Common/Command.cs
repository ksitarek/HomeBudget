using MediatR;

namespace HomeBudget.Application.Commands.Common
{
    public abstract class Command : IRequest<CommandResult> { }
}