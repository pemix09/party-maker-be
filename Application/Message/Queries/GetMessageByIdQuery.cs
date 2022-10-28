namespace Application.Message.Queries
{
    using MediatR;
    using Core.Models;
    using Persistence.Services.Database;
    using Application.Message.Validators;
    using FluentValidation;

    public class GetMessageByIdQuery : IRequest<Message>
    {
        public long Id { get; init; }

        public class Handler : IRequestHandler<GetMessageByIdQuery, Message>
        {
            private readonly IMessageService messageService;
            public Handler(IMessageService _messageService)
            {
                messageService = _messageService;
            }

            public async Task<Message> Handle(GetMessageByIdQuery request, CancellationToken cancellationToken)
            {
                await new GetMessageByIdValidator().ValidateAndThrowAsync(request, cancellationToken);

                Message message = await messageService.GetByIdFromDataBase(request.Id);

                return message;
            }
        }
    }
}
