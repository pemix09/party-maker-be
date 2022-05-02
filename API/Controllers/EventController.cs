namespace API.Controllers
{

    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Application.Event.Commands;
    
    [Route("[controller]/[action]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IMediator Mediator;

        public EventController(IMediator mediator)
        {
            Mediator = mediator;
        }

        [HttpPost]
        public async Task<Unit> Create([FromBody] CreateEventCommand command) =>
            await Mediator.Send(command);

        [HttpPut]
        public async Task<Unit> Update([FromBody] UpdateEventCommand command) =>
            await Mediator.Send(command);
    }
}