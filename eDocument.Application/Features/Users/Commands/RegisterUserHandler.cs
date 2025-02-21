using eDocument.Domain.Entities;
using eDocument.Domain.Interfaces;
using MediatR;

namespace eDocument.Application.Features.Users.Commands
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterUserHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = User.Register(request.Email, _passwordHasher.HashPassword(request.Password), request.FirstName, request.LastName);
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
        }
    }
}
