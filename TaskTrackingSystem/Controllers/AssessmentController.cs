using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskTrackingSystem.Application.Assessments.Commands.Create;
using TaskTrackingSystem.Application.Assessments.Queries;

namespace TaskTrackingSystem.Controllers
{
    public class AssessmentController : ApiBaseController
    {
        private readonly IMediator _mediator;

        public AssessmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAssessmentCommand command)
        {
            var assessmentId = await _mediator.Send(command);
            return Ok(assessmentId);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            var result = await _mediator.Send(new GetAssessmentByUserQuery { UserId = userId });
            return Ok(result);
        }
    }
}
