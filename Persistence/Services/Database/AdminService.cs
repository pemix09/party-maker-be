using Core.Models;
using Microsoft.AspNetCore.Identity;
using Persistence.DbContext;

namespace Persistence.Services.Database;

public class AdminService : ServiceBase
{
    private IConfiguration config;
    private UserManager<AppUser> userManager;
    public AdminService(PartyMakerDbContext _context, IConfiguration _config, UserManager<AppUser> _userManager) : base(_context)
    {
        config = _config;
        userManager = _userManager;
    }

    public async Task AddSuperAdmins()
    {
        List<string> superAdmins = config.GetSection("SuperAdmins").Get<List<string>>();
        List<string> allRolesNames = config.GetSection("Roles").Get<List<string>>();

        foreach (string superAdmin in superAdmins)
        {
            var suAdmin = await userManager.FindByEmailAsync(superAdmin);
            if (suAdmin == null) continue;
            foreach (var role in allRolesNames)
            {
                if (! await userManager.IsInRoleAsync(suAdmin, role))
                {
                    await userManager.AddToRoleAsync(suAdmin, role);
                }
            }   
        }
    }
}