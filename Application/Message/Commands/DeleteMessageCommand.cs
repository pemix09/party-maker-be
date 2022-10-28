using Application.Message.Validators;
using FluentValidation;
using MediatR;
using Persistence.Services.Database;

namespace Application.Message.Commands
{
    public class DeleteMessageCommand : IRequest<Unit>
    {
        public long Id { get; init; }

        public class Handler : IRequestHandler<DeleteMessageCommand, Unit>
        {
            private readonly IMessageService messageService;
            public Handler(IMessageService _messageService)
            {
                messageService = _messageService;
            }

            public async Task<Unit> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
            {
                await new DeleteMessageValidator().ValidateAndThrowAsync(request, cancellationToken);

                await messageService.DeleteFromDataBase(request.Id);

                return Unit.Value;
            }
        }
    }
}
