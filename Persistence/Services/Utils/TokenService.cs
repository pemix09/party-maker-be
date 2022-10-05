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

        public async Task<string> CreateAccessToken(AppUser _user)
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

            //Nullable claims
            if(_user.PhoneNumber != null)
            {
                claims.Add(new Claim(ClaimTypes.MobilePhone, _user.PhoneNumber));
            }

            //key for encryption, ensures that token is valid
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(5),
                signingCredentials: credentials
            );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public RefreshToken CreateRefreshToken()
        {
            RefreshToken token = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(15),
                Created = DateTime.Now
            };

            SetRefreshToken(token);
            return token;
        }

        private void SetRefreshToken(RefreshToken _newRefreshToken)
        {
            var context = httpContextAccessor.HttpContext;
            
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = _newRefreshToken.Expires
            };
            context.Response.Cookies.Append("RefreshToken", _newRefreshToken.Token, cookieOptions);
        }
    }

    
}