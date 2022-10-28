using Application.User.Validators;
using FluentValidation;
using MediatR;
using Persistence.Services.Database;

namespace Application.User.Commands
{
    public class LoginUserCommand : IRequest<string>
    {
        public string Email { get; init; }
        public string Password { get; init; }
        public class Handler : IRequestHandler<LoginUserCommand, string>
        {
            IUserService userService { get; init; }
            public Handler(IUserService _userService)
            {
                userService = _userService;
            }
            public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
            {
                await new LoginUserValidator().ValidateAndThrowAsync(request, cancellationToken);
                string token = await userService.Login(request.Email, request.Password);

                return token;
            }
        }
    }
}
