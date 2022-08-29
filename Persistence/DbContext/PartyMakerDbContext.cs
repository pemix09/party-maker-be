using Core.Models;

namespace Persistence.DbContext
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Persistence.ModelConfiguration;

    public class PartyMakerDbContext : IdentityDbContext<AppUser,IdentityRole,string>
    {
        public DbSet<Ban> Bans { get; set; }
        public DbSet<EntrancePass> EntrancePasses { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MusicGenre> MusicGenres { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Log> Logs { get; set; }

        public PartyMakerDbContext(DbContextOptions<PartyMakerDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PartyMakerDbContext).Assembly);
            modelBuilder.ApplyConfiguration(new LogConfiguration());
        }
        
    }
}
