namespace Application.Message.Commands
{
    using Application.Message.Validators;
    using FluentValidation;
    using MediatR;
    using Persistence.Services.Database;
    using Core.Models;

    public class CreateMessageCommand : IRequest<Unit>
    {
        public string Content { get; init; }
        public int EventId { get; init; }
        public string ReceiverId { get; init; }
        public string SenderId { get; init; }

        public class Handler : IRequestHandler<CreateMessageCommand, Unit>
        {
            private readonly MessageService messageService;
            public Handler(MessageService _messageService)
            {
                messageService = _messageService;
            }
            public async Task<Unit> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
            {
                await new CreateMessageValidator().ValidateAndThrowAsync(request, cancellationToken);

                Message message = Message.Create(request.SenderId, request.ReceiverId, request.EventId, request.Content);
                await messageService.AddToDataBase(message);

                return Unit.Value;
            }
        }
    }
}
