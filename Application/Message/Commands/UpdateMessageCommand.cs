using MediatR;

namespace Application.Message.Commands
{
    using Application.Message.Validators;
    using Core.Models;
    using FluentValidation;
    using Persistence.Services.Database;
    using System.Threading;
    using System.Threading.Tasks;

    public class UpdateMessageCommand : IRequest<Unit>
    {
        public string NewContent { get; init; }
        public long MessageId { get; init; }

        public class Handler : IRequestHandler<UpdateMessageCommand, Unit>
        {
            private readonly MessageService messageService;
            public Handler(MessageService _messageService)
            {
                messageService = _messageService;
            }
            public async Task<Unit> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
            {
                await new UpdateMessageValidator().ValidateAndThrowAsync(request, cancellationToken);

                Message message = await messageService.GetByIdFromDataBase(request.MessageId);
                message.SetContent(request.NewContent);
                await messageService.UpdateInDataBase(message);
                
                return Unit.Value;
            }
        }
    }
}
