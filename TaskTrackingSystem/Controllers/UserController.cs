using Microsoft.AspNetCore.Mvc;
using TaskTrackingSystem.Application.Users.Commands.Create;
using TaskTrackingSystem.Application.Users.Commands.Delete;
using TaskTrackingSystem.Application.Users.Commands.Update;
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertUserCommand command)
        {
            var id = await Mediator.Send(command);
            return NoContent();
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(Guid id)
        {
            await Mediator.Send(new DeleteUserCommand { Id = id });
            return NoContent();
        }
        
    }
}
