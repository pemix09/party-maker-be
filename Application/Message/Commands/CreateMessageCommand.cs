namespace Application.Message.Commands
{
    using Application.Message.Validators;
    using FluentValidation;
    using MediatR;
    using Persistence.Services.Database;
    using Core.Models;

    public class CreateMessageCommand : IRequest<Message>
    {
        public string Content { get; init; }
        public int EventId { get; init; }
        public string ReceiverId { get; init; }
        public class Handler : IRequestHandler<CreateMessageCommand, Message>
        {
            private readonly MessageService messageService;
            private readonly IUserService userService;
            public Handler(MessageService _messageService, IUserService _userService)
            {
                messageService = _messageService;
                userService = _userService;
            }
            public async Task<Message> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
            {
                await new CreateMessageValidator().ValidateAndThrowAsync(request, cancellationToken);

                AppUser sender = await  userService.GetCurrentlySignedIn();
                Message message = Message.Create(sender.Id, request.ReceiverId, request.EventId, request.Content);
                await messageService.AddToDataBase(message);

                return message;
            }
        }
    }
}
