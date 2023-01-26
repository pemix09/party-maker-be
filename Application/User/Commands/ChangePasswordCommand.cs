using Application.User.Validators;
using Core.Models;
using MediatR;
using Persistence.Services.Database;
using Persistence.Exceptions;

namespace Application.User.Commands
{
    public class ChangePasswordCommand : IRequest<Unit>
    {
        public string OldPassword { get; init; }
        public string NewPassword { get; init; }

        public class Handler : IRequestHandler<ChangePasswordCommand, Unit>
        {
            private IUserService userService;
            public Handler(IUserService _userService)
            {
                userService = _userService;
            }
            public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
            {
                var user = await userService.GetCurrentlySignedIn();
                if (user == null)
                {
                    throw new UserNotAuthenticatedException();
                }

                await userService.ChangePassword(user.Id, request.OldPassword, request.NewPassword);
                return Unit.Value;
            }
        }
    }
}
