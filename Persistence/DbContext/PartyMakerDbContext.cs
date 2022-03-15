namespace Persistence.DbContext
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class PartyMakerDbContext : IdentityDbContext<IdentityUser,IdentityRole,string>
    {
        

    }
}
