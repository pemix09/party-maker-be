using Core.Models;
using Microsoft.AspNetCore.Identity;
using Persistence.DbContext;

namespace Persistence.Services.Database
{
    public class UserService : ServiceBase
    {
        private UserManager<AppUser> userManager { get; init; }
        private RoleManager<IdentityRole> roleManager { get; init; }
        public UserService(
            PartyMakerDbContext _context, 
            UserManager<AppUser> _userManager, 
            RoleManager<IdentityRole> _roleManager) : base(_context) 
        {
            userManager = _userManager;
            roleManager = _roleManager;
        }
    }
}
