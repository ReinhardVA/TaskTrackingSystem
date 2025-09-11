using Microsoft.AspNetCore.Mvc;
using TaskTrackingSystem.Application.Users.Commands.Create;
using TaskTrackingSystem.Application.Users.Queries.GelAllUsers;
using TaskTrackingSystem.Application.Users.Queries.GetUserById;

namespace TaskTrackingSystem.Controllers
{
    public class UserController : ApiBaseController
    {
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserByIdVm>> Get(Guid id)
        {
            var vm = await Mediator.Send(new GetUserByIdQuery { Id = id });
            return Ok(vm);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserListVm>> GetList()
        {
            var vm = await Mediator.Send(new GetAllUsersQuery());
            return Ok(vm);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

    }
}
