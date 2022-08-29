using MediatR;

namespace API.Controllers
{
    public class EntrancePassController : BaseCRUDController
    {
        public EntrancePassController(IMediator _mediator) : base(_mediator)
        {
        }
    }
}
