using Core.UtilityClasses;

namespace Persistence.Services.Utils
{
    using Core.Models;
    using Microsoft.AspNetCore.Identity;
    using System.Security.Claims;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Cryptography;
    using System.Text;
    using Microsoft.IdentityModel.Tokens;

    public class TokenService
    {
        private UserManager<AppUser> userManager;
        private IConfiguration configuration;
        private IHttpContextAccessor httpContextAccessor;
        public TokenService(UserManager<AppUser> _userManager, IConfiguration _configuration, IHttpContextAccessor _httpContextAccesor)
        {
            userManager = _userManager;
            configuration = _configuration;
            httpContextAccessor = _httpContextAccesor;
        }

        public async Task<AccessToken> CreateAccessToken(AppUser _user)
        {
            //list for storing user claims
            ICollection<Claim> claims = new List<Claim>();

            //Add user's roles claims
            IList<string> roles = await userManager.GetRolesAsync(_user);
            foreach (string role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            //user specific claims
            claims.Add(new Claim(ClaimTypes.Name, _user.UserName));
            claims.Add(new Claim(ClaimTypes.Email, _user.Email));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, _user.Id));

            //Nullable claims
            if(_user.PhoneNumber != null)
            {
                claims.Add(new Claim(ClaimTypes.MobilePhone, _user.PhoneNumber));
            }

            //key for encryption, ensures that token is valid
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenExpirationDate = DateTime.Now.AddDays(5);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: tokenExpirationDate,
                signingCredentials: credentials
            );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            var accessToken = new AccessToken
            {
                Token = jwt,
                Expires = tokenExpirationDate,
                Created = DateTime.Now
            };

            return accessToken;
        }

        public RefreshToken CreateRefreshToken()
        {
            RefreshToken token = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = GetNewRefreshTokenExpirationDate(),
                Created = DateTime.Now
            };

            return token;
        }

        public DateTime GetNewRefreshTokenExpirationDate()
        {
            return DateTime.Now.AddDays(15);
        }

    }

    
}