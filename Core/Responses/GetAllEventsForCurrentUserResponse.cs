using Core.Models;

namespace Core.Responses
{
    public class GetAllEventsForCurrentUserResponse
    {
        public List<Event> Followed { get; set; }
        public List<Event> Organized { get; set; }
    }
}
