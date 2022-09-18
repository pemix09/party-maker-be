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
        public TokenService(UserManager<AppUser> _userManager, IConfiguration _configuration)
        {
            userManager = _userManager;
            configuration = _configuration;
        }

        //not sure if I'm going to use it, and this method works, but return type is wrongs
        public async Task<string> CreateToken(AppUser _user)
        {
            //get user claims
            ICollection<Claim> claims = await userManager.GetClaimsAsync(_user);

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
    }
}