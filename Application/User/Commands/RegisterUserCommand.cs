using Application.User.Validators;
using Core.Models;
using FluentValidation;
using MediatR;
using Persistence.Services.Database;

namespace Application.User.Commands
{
    public class RegisterUserCommand : IRequest<Unit>
    {
        public string Email { get; init; }
        public string Password { get; init; }
        public string UserName { get; init; }

        public class Handler : IRequestHandler<RegisterUserCommand, Unit>
        {
            private IUserService userService;
            public Handler(IUserService _userService)
            {
                userService = _userService;
            }
            public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                await new RegisterUserValidator().ValidateAndThrowAsync(request, cancellationToken);

                AppUser newUser = AppUser.Create(request.Email, request.UserName);

                await userService.Register(newUser, request.Password);

                return Unit.Value;
            }
        }
    }
}
