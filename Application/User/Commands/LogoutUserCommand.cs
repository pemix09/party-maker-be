using MediatR;
using Persistence.Services.Database;

namespace Application.User.Commands
{
    public class LogoutUserCommand : IRequest<Unit>
    {
        public class Handler : IRequestHandler<LogoutUserCommand, Unit>
        {
            UserService userService { get; init; }
            public Handler(UserService _userService)
            {
                userService = _userService;
            }

            public async Task<Unit> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
            {
                await userService.Logout();
                return Unit.Value;
            }
        }
    }
}
