using MediatR;

namespace API.Controllers
{
    public class BanController : BaseCRUDController
    {
        public BanController(IMediator _mediator) : base(_mediator) { }
    }
}
