using Core.Models;
using Core.UtilityClasses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Persistence.Exceptions;
using Persistence.Services.Database;
using Persistence.Services.Utils;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        private IConfiguration config { get; init; }
        private UserManager<AppUser> userManager { get; init; }
        public Handler(IUserService _userService, 
                        IHttpContextAccessor _httpContextAccesor, 
                        TokenService _tokenService, 
                        IConfiguration _config,
                        UserManager<AppUser> _userManager)
        {
            userService = _userService;
            contextAccesor = _httpContextAccesor;
            tokenService = _tokenService;
            config = _config;
            userManager= _userManager;
        }
        public async Task<AccessToken> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var jwtSecret = config["JWT:Secret"];
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, 
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
                RequireExpirationTime = true,
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(request.AccessToken, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null)
                throw new SecurityTokenException("Invalid token");

            var user = await userService.GetUserById(userManager.GetUserId(principal));

            if (user.RefreshToken.Equals(string.Empty))
            {
                throw new UserNotAuthenticatedException();
            }
            else if (!user.RefreshToken.Equals(request.RefreshToken))
            {
                throw new InvalidRefreshTokenException();
            }
            else if (user.RefreshTokenExpires < DateTime.Now)
            {
                throw new TokenExpiredException();
            }

            user.RefreshTokenExpires = tokenService.GetNewRefreshTokenExpirationDate();
            var accessToken = await tokenService.CreateAccessToken(user);
            return accessToken;
        }
    }
}