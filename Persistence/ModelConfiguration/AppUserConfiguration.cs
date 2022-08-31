using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.ModelConfiguration
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(user => user.Id);

            builder.HasMany(user => user.Followed)
                .WithMany(e => e.Participants)
                .UsingEntity("EventParticipant");

            builder.HasOne(user => user.Ban)
                .WithOne(ban => ban.BannedUser)
                .HasForeignKey<Ban>()
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}