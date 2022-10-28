using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

//below attribute is for jwt authorization
[Authorize(AuthenticationSchemes = "Bearer")]
[Route("[controller]/[action]")]
[ApiController]
public class AdminController : BaseCRUDController
{
    public AdminController(IMediator _mediator) : base(_mediator)
    {
    }
}