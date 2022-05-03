using Application.Event.Queries;
using Core.Models;

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

        [HttpDelete]
        public async Task<Unit> Delete([FromQuery] DeleteEventCommand command) =>
            await Mediator.Send(command);

        [HttpGet]
        public async Task<Event> GetById([FromQuery] GetEventByIdQuery query) =>
            await Mediator.Send(query);

        [HttpGet]
        public async Task<IEnumerable<Event>> GetAll([FromQuery] GetAllEventsQuery query) =>
            await Mediator.Send(query);
    }
}