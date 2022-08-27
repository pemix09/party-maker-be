using Core.Dto;
using Core.Models;
using MediatR;
using Persistence.Exceptions;
using Persistence.Services.Database;

namespace Application.User.Queries
{
    public class GetLoggedUserQuery : IRequest<AppUserDto>
    {
        public class Handler : IRequestHandler<GetLoggedUserQuery, AppUserDto>
        {
            UserService userService { get; init; }
            public Handler(UserService _userService)
            {
                userService = _userService;
            }

            public async Task<AppUserDto> Handle(GetLoggedUserQuery request, CancellationToken cancellationToken)
            {
                AppUser user = await userService.GetCurrent();

                if(user == null)
                {
                    throw new UserNotLoggedException();
                }
                AppUserDto userDto = new AppUserDto(user.Id,user.UserName, user.Email, user.Photo);

                return userDto;
            }
        }
    }
}
