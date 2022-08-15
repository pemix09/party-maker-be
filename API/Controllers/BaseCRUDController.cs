using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BaseCRUDController : ControllerBase
    {
        protected readonly IMediator mediator;
        public BaseCRUDController(IMediator _mediator)
        {
            mediator = _mediator;
        }
    }
}
