using Core.UtilityClasses;
using MediatR;
using Persistence.Exceptions;
using Persistence.Services.Database;
using Persistence.Services.Utils;

namespace Application.User.Commands;

public class RefreshTokenCommand : IRequest<AccessToken>
{
    public string AccessToken { get; init; }
    public string RefreshToken { get; init; }
    public class Handler : IRequestHandler<RefreshTokenCommand, AccessToken>
    {
        private IUserService userService { get; init; }
        private IHttpContextAccessor contextAccesor { get; init; }
        private TokenService tokenService { get; init; }
        public Handler(IUserService _userService, IHttpContextAccessor _httpContextAccesor, TokenService _tokenService)
        {
            userService = _userService;
            contextAccesor = _httpContextAccesor;
            tokenService = _tokenService;
        }
        public async Task<AccessToken> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = contextAccesor.HttpContext.Request.Cookies["RefreshToken"];
            var user = await userService.GetCurrentlySignedIn();

            if (user.RefreshToken.Equals(string.Empty))
            {
                throw new UserNotAuthenticatedException();
            }
            else if (!user.RefreshToken.Equals(refreshToken))
            {
                throw new InvalidRefreshTokenException();
            }
            else if (user.RefreshTokenExpires < DateTime.Now)
            {
                throw new TokenExpiredException();
            }

            var accessToken = await tokenService.CreateAccessToken(user);
            return accessToken;
        }
    }
}