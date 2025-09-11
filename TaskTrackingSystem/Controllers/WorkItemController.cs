using Microsoft.AspNetCore.Mvc;
using TaskTrackingSystem.Application.WorkItems.Commands.Create;
using TaskTrackingSystem.Application.WorkItems.Queries.GetWorkItemByIdQuery;

namespace TaskTrackingSystem.Controllers
{
    public class WorkItemController : ApiBaseController
    {

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WorkItemByIdVm>> GetById(Guid id)
        {
            var result = await Mediator.Send(new GetWorkItemByIdQuery { Id = id});

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWorkItemCommand command)
        {
            var id = await Mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new {id}, id);
        }
    }
}
