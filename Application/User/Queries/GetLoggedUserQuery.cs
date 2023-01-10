using AutoMapper;
using Core.Dto;
using Core.Enums;
using Core.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence.Exceptions;
using Persistence.Services.Database;

namespace Application.User.Queries
{
    public class GetLoggedUserQuery : IRequest<AppUserDto>
    {
        public class Handler : IRequestHandler<GetLoggedUserQuery, AppUserDto>
        {
            IUserService userService { get; init; }
            IMapper mapper { get; init; }
            UserManager<AppUser> userManager { get; init; }

            public Handler(IUserService _userService, IMapper _mapper, UserManager<AppUser> _userManager)
            {
                userService = _userService;
                mapper = _mapper;
                userManager= _userManager;
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
                

                if(await userManager.IsInRoleAsync(user, RoleEnum.SuperAdmin.ToString()))
                {
                    userDto.Role = RoleEnum.SuperAdmin.ToString();
                }
                else if(await userManager.IsInRoleAsync(user, RoleEnum.Admin.ToString()))
                {
                    userDto.Role = RoleEnum.Admin.ToString();
                }
                else if(await userManager.IsInRoleAsync(user, RoleEnum.User.ToString()))
                {
                    userDto.Role = RoleEnum.User.ToString();
                }

                return userDto;
            }
        }
    }
}
