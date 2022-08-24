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
        public async Task<ActionResult> Create([FromBody] CreateMessageCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateMessageCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] DeleteMessageCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<Message>> GetById([FromQuery] GetMessageByIdQuery query)
        {
            Message message = await mediator.Send(query);
            return Ok(message);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetAllByUser([FromQuery] GetAllMessagesQuery query)
        {
            IEnumerable<Message> messages = await mediator.Send(query);
            return Ok(messages);

        }
    }
}
