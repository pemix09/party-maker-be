using Application.User.Validators;
using Core.Models;
using MediatR;
using Persistence.Services.Database;

namespace Application.User.Commands
{
    public class ChangePasswordCommand : IRequest<Unit>
    {
        public string UserId { get; init; }
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
                await userService.ChangePassword(request.UserId, request.OldPassword, request.NewPassword);

                return Unit.Value;
            }
        }
    }
}
