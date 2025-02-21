using MediatR;

namespace eDocument.Application.Features.Users.Commands
{
    public record RegisterUserCommand(string Email, string Password, string FirstName, string LastName) : IRequest;
}
