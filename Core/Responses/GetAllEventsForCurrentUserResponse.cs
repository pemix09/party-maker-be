using Core.Dto;
using Core.Models;

namespace Core.Responses
{
    public class GetAllEventsForCurrentUserResponse
    {
        public List<EventDto> Followed { get; set; }
        public List<EventDto> Organized { get; set; }
        public List<EventDto> Participates { get; set; }
    }
}
