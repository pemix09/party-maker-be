using Core.Dto;
using MediatR;
using Persistence.Services.Database;

namespace Application.User.Queries
{
    public class GetManyUsersQuery : IRequest<IEnumerable<AppUserDto>>
    {
        public List<string> UserIds { get; set; }
        public class Handler : IRequestHandler<GetManyUsersQuery, IEnumerable<AppUserDto>>
        {
            private readonly IUserService userService;
            public Handler(IUserService _userService)
            {
                userService = _userService;
            }
            public async Task<IEnumerable<AppUserDto>> Handle(GetManyUsersQuery request, CancellationToken cancellationToken)
            {
                return await userService.GetMany(request.UserIds);
            }
        }
    }
}
