using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TaskTrackingSystem.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ApiBaseController : ControllerBase
    {
        private ISender? _mediator;
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    }
}
