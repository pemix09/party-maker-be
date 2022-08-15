using Application.Event.Queries;
using Core.Models;

namespace API.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Application.Event.Commands;
    
    [Route("[controller]/[action]")]
    [ApiController]
    public class EventController : BaseCRUDController
    {
        public EventController(IMediator _mediator) : base(_mediator) { }

        [HttpPost]
        public async Task<Unit> Create([FromBody] CreateEventCommand command) =>
            await mediator.Send(command);

        [HttpPut]
        public async Task<Unit> Update([FromBody] UpdateEventCommand command) =>
            await mediator.Send(command);

        [HttpDelete]
        public async Task<Unit> Delete([FromQuery] DeleteEventCommand command) =>
            await mediator.Send(command);

        [HttpGet]
        public async Task<Event> GetById([FromQuery] GetEventByIdQuery query) =>
            await mediator.Send(query);

        [HttpGet]
        public async Task<IEnumerable<Event>> GetAll([FromQuery] GetAllEventsQuery query) =>
            await mediator.Send(query);
    }
}