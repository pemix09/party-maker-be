namespace API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Core.Models;
    using MediatR;
    using Application.Message.Commands;
    using Application.Message.Queries;

    [Route("[controller]/[action]")]
    [ApiController]
    public class MessageController : BaseCRUDController
    {
        public MessageController(IMediator _mediator) : base(_mediator) { }

        [HttpPost]
        public async Task<Unit> Create([FromBody] CreateMessageCommand command) =>
            await mediator.Send(command);

        [HttpPut]
        public async Task<Unit> Update([FromBody] UpdateMessageCommand command) =>
            await mediator.Send(command);

        [HttpDelete]
        public async Task<Unit> Delete([FromQuery] DeleteMessageCommand command) =>
            await mediator.Send(command);

        [HttpGet]
        public async Task<Message> GetById([FromQuery] GetMessageByIdQuery query) =>
            await mediator.Send(query);

        [HttpGet]
        public async Task<IEnumerable<Message>> GetAllByUser([FromQuery] GetAllMessagesQuery query) =>
            await mediator.Send(query);
    }
}
