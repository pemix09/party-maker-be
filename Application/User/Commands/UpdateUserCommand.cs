using Core.Dto;
using MediatR;
using Persistence.Services.Database;
using Persistence.Exceptions;

namespace Application.User.Commands
{
    public class UpdateUserCommand : IRequest<Unit>
    {
        public string? newUserName { get; set; }
        public string? newPhoto { get; set; }

        public class Handler : IRequestHandler<UpdateUserCommand, Unit>
        {
            private IUserService userService;
            public Handler(IUserService _userService)
            {
                userService = _userService;
            }
            public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                var user = await userService.GetCurrentlySignedIn();
                if(user == null)
                {
                    throw new UserNotAuthenticatedException();
                }

                if(request.newUserName != null)
                {
                    user.UserName = request.newUserName;
                }
                if(request.newPhoto != null)
                {
                    user.Photo = request.newPhoto;
                }

                await userService.UpdateUser(user);

                return Unit.Value;
            }
        }
    }
}
