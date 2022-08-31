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
            builder.HasOne(e => e.Organizer)
                .WithMany(b => b.OrganizedEvents)
                .HasForeignKey()// entity framework will automatically replace AppUser Organizer for OrganizerId :D(take a look at database)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.MusicGenre);

            builder.Property(x => x.Description)
                .IsRequired();

            builder.Property(x => x.Date)
                .IsRequired();
            
            builder.Property(x => x.Photo)
                .IsRequired();

            builder.Property(x => x.Place)
                .IsRequired();

            //if we want to delete event from user on cascade, we have to 
            //make another table, to remove many to many relation
            //however entity framework will create this table automatically, beacuse of this relation!
            // !but how to create on delete action?
            builder.HasMany(x => x.Participants)
                .WithMany(b => b.TakesPart)
                .UsingEntity("EventParticipant");

                
        }
    }
}