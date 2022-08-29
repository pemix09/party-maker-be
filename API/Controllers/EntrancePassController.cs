using Application.EntrancePass.Commands;
using Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class EntrancePassController : BaseCRUDController
    {
        public EntrancePassController(IMediator _mediator) : base(_mediator){}
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateEntrancePassCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateEntrancePassCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] DeleteEntrancePassCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<Event>> GetById([FromQuery] GetEntrancePassByIdQuery query)
        {
            EntrancePass pass = await mediator.Send(query);
            return Ok(pass);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EntrancePass>>> GetAll([FromQuery] GetAllEntrancePassesQuery query)
        {
            IEnumerable<EntrancePass> passes = await mediator.Send(query);
            return Ok(passes);
        }
    }
}
