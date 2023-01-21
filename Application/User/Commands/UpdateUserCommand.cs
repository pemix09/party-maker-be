using Core.Dto;
using MediatR;
using Persistence.Services.Database;

namespace Application.User.Commands
{
    public class UpdateUserCommand : IRequest<Unit>
    {
        public AppUserDto User { get; set; }

        public class Handler : IRequestHandler<UpdateUserCommand, Unit>
        {
            private IUserService userService;
            public Handler(IUserService _userService)
            {
                userService = _userService;
            }
            public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                await userService.UpdateUser(request.User);

                return Unit.Value;
            }
        }
    }
}
