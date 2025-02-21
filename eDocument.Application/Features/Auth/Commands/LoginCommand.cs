using MediatR;

namespace eDocument.Application.Features.Auth.Commands
{
    public record LoginCommand(string Email, string Password) : IRequest<AuthenticationResponse>;
}
