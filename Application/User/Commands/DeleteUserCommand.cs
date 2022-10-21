using MediatR;
using Persistence.Services.Database;

namespace Application.User.Commands
{
    public class DeleteUserCommand : IRequest<Unit>
    {
        public class Handler : IRequestHandler<DeleteUserCommand, Unit>
        {
            IUserService userService { get; init; }
            public Handler(IUserService _userService)
            {
                userService = _userService;
            }

            public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                await userService.DeleteCurrent();

                return Unit.Value;
            }
        }
    }
}
