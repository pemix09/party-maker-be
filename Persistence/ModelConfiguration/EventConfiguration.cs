namespace Persistence.ModelConfiguration
{
    using Core.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;

    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            //On delete cascada, when Event is deleted, it should be deleted
            //from Organizer as well
            builder.HasOne(x => x.Organizer)
                .WithMany(b => b.OrganizedEvents)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.MusicGenre)
                .IsRequired(false);

            builder.Property(x => x.Description)
                .IsRequired();

            builder.Property(x => x.Date)
                .IsRequired();
            
            builder.Property(x => x.Photo)
                .IsRequired();

            builder.Property(x => x.Place)
                .IsRequired();

            builder.HasMany(x => x.Participants)
                .WithMany(b => b.TakesPart);

                
        }
    }
}