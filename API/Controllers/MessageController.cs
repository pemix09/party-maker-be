using Microsoft.AspNetCore.Mvc;
using Core.Models;
using MediatR;

namespace API.Controllers
{
    public class MessageController : BaseCRUDController
    {
        public MessageController(IMediator _mediator) : base(_mediator) { }
    }
}
