using AutoMapper;
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
            IMapper mapper { get; init; }

            public Handler(UserService _userService, IMapper _mapper)
            {
                userService = _userService;
                mapper = _mapper;
            }

            public async Task<AppUserDto> Handle(GetLoggedUserQuery request, CancellationToken cancellationToken)
            {
                AppUser user = await userService.GetCurrentlySignedIn();

                if(user == null)
                {
                    throw new UserNotLoggedException();
                }
                //using Data transfer object, to not return the whole user
                AppUserDto userDto = mapper.Map<AppUserDto>(user);

                return userDto;
            }
        }
    }
}
