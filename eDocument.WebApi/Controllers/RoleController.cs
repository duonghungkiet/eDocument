using eDocument.Application.Features.Roles.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eDocument.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoleCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
