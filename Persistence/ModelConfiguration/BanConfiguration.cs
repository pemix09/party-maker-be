using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.ModelConfiguration
{
    public class BanConfiguration : IEntityTypeConfiguration<Ban>
    {
        public void Configure(EntityTypeBuilder<Ban> builder)
        {
            //Primary key with auto-increment
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            //If the ban is deleted, ban property in user will be set to null
            builder.HasOne(x => x.BannedUser)
                .WithOne(b => b.Ban)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property(x => x.Reason)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.ResponsibleAdmin)
                .IsRequired();

            builder.Property(x => x.Start)
                .IsRequired()
                .HasDefaultValue(DateTime.Now);

            builder.Property(x => x.End)
                .IsRequired();
        }
    }
}