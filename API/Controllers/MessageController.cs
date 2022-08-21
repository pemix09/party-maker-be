namespace API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Core.Models;
    using MediatR;
    public class MessageController : BaseCRUDController
    {
        public MessageController(IMediator _mediator) : base(_mediator) { }

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
        public async Task<Message> GetById([FromQuery] GetEventByIdQuery query) =>
            await mediator.Send(query);

        [HttpGet]
        public async Task<IEnumerable<Message>> GetAllByUser([FromQuery] GetAllEventsQuery query) =>
            await mediator.Send(query);
    }
}
