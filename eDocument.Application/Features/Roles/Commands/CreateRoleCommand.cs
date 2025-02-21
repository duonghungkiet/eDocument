using MediatR;

namespace eDocument.Application.Features.Roles.Commands
{
    public record CreateRoleCommand(string Name, string Description) : IRequest;
}
