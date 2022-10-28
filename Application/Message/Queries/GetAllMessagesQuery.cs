using MediatR;

namespace Application.Message.Queries
{
    using Core.Models;
    using Persistence.Services.Database;

    public class GetAllMessagesQuery : IRequest<IEnumerable<Message>>
    {
        public class Handler : IRequestHandler<GetAllMessagesQuery, IEnumerable<Message>>
        {
            private readonly IMessageService messageService;
            public Handler(IMessageService _messageService)
            {
                messageService = _messageService;
            }

            public async Task<IEnumerable<Message>> Handle(GetAllMessagesQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<Message> messages = await messageService.GetAllFromDataBase();

                return messages;
            }
        }
    }
}
