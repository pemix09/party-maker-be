using MediatR;

namespace Application.Message.Queries
{
    using Core.Models;
    using Core.Responses;
    using Persistence.Services.Database;

    public class GetAllMessagesQuery : IRequest<IEnumerable<Message>>
    {
        public int eventId { get; set; }
        public class Handler : IRequestHandler<GetAllMessagesQuery, IEnumerable<Message>>
        {
            private readonly IMessageService messageService;
            private readonly IUserService userService;
            public Handler(IMessageService _messageService, IUserService _userService)
            {
                messageService = _messageService;
                userService = _userService;
            }

            public async Task<IEnumerable<Message>> Handle(GetAllMessagesQuery request, CancellationToken cancellationToken)
            {
                return await messageService.GetAllForEvent(request.eventId);
            }
        }
    }
}
