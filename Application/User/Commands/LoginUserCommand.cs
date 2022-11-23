using Application.User.Validators;
using Core.UtilityClasses;
using FluentValidation;
using MediatR;
using Persistence.Services.Database;

namespace Application.User.Commands
{
    public class LoginUserCommand : IRequest<LoginResponse>
    {
        public string Email { get; init; }
        public string Password { get; init; }
        public class Handler : IRequestHandler<LoginUserCommand, LoginResponse>
        {
            IUserService userService { get; init; }
            public Handler(IUserService _userService)
            {
                userService = _userService;
            }
            public async Task<LoginResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
            {
                await new LoginUserValidator().ValidateAndThrowAsync(request, cancellationToken);
                var loginTokenResponse = await userService.Login(request.Email, request.Password);

                return loginTokenResponse;
            }
        }
    }
}
