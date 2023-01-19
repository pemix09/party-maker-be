using Core.Models;

namespace Core.Responses
{
    public class GetAllMessagesForUser
    {
        public List<Message> UserMessages { get; set; }
        public List<Message> EventMessages { get; set;}
    }
}
