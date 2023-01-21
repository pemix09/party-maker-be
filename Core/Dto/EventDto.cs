using System.ComponentModel.DataAnnotations;

namespace Core.Dto
{
    public class EventDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Place { get; set; }
        public string OrganizerId { get; set; }
        public List<string>? ParticipatorsIds { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int PassId { get; set; }
        public string Photo { get; set; }
        public int MusicGenreId { get; set; }
        public string Type { get; set; }
        public string LastMessage { get; set; }
        public DateTimeOffset LastMessageTime { get; set; }
    }
}
