using eDocument.Domain.Entities;
using eDocument.Domain.Interfaces;
using MediatR;

namespace eDocument.Application.Features.Roles.Commands
{
    public class CreateRoleHandler : IRequestHandler<CreateRoleCommand>
    {
        private readonly IRoleRepository _roleRepository;

        public CreateRoleHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = new Role(request.Name, request.Description, null);
            await _roleRepository.AddAsync(role);
            await _roleRepository.SaveChangesAsync();
        }
    }
}
