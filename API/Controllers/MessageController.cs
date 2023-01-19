using Infrastructure;

namespace API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Core.Models;
    using MediatR;
    using Application.Message.Commands;
    using Application.Message.Queries;
    using Microsoft.AspNetCore.Authorization;
    using Core.Responses;

    //below attribute is for jwt authorization
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("[controller]/[action]")]
    [ApiController]
    public class MessageController : BaseCRUDController
    {
        public MessageController(IMediator _mediator) : base(_mediator) { }

        [HttpPost, Authorize(Roles = "User")]
        public async Task<ActionResult> Create([FromBody] CreateMessageCommand command)
        {
            var message = await mediator.Send(command);
            var notification = MessageSentNotification.CreateFromMessage(message);
            await mediator.Publish(notification);
            return Ok();
        }

        [HttpPut, Authorize(Roles = "User")]
        public async Task<ActionResult> Update([FromBody] UpdateMessageCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }

        [HttpDelete, Authorize(Roles = "User")]
        public async Task<ActionResult> Delete([FromQuery] DeleteMessageCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }

        [HttpGet, Authorize(Roles = "User")]
        public async Task<ActionResult<Message>> GetById([FromQuery] GetMessageByIdQuery query)
        {
            Message message = await mediator.Send(query);
            return Ok(message);
        }

        [HttpGet, Authorize(Roles = "User")]
        public async Task<ActionResult<IEnumerable<Message>>> GetAllForParty([FromQuery] GetAllMessagesQuery query)
        {
            return Ok(await mediator.Send(query));
        }
    }
}
