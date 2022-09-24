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
    }
}