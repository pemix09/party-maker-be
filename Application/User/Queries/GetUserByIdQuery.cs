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
    public class GetUserByIdQuery : IRequest<AppUserDto>
    {
        public string UserId { get; set; }
        public class Handler : IRequestHandler<GetUserByIdQuery, AppUserDto>
        {
            IUserService userService { get; init; }
            IMapper mapper { get; init; }

            public Handler(IUserService _userService, IMapper _mapper)
            {
                userService = _userService;
                mapper = _mapper;
            }

            public async Task<AppUserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
            {
                var user = await userService.GetUserById(request.UserId);
                return mapper.Map<AppUserDto>(user);
            }
        }
    }
}
