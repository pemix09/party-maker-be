using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.ModelConfiguration
{
    public class LogConfiguration : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Message)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(256);

            builder.Property(x => x.StackTrace)
                .HasColumnType("varchar");

            builder.Property(x => x.LogType)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(100);

        }
    }
}
