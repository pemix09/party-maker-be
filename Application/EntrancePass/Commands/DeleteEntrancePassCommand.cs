using Application.EntrancePass.Validators;
using FluentValidation;
using MediatR;
using Persistence.Services.Database;

namespace Application.EntrancePass.Commands
{
    public class DeleteEntrancePassCommand : IRequest<Unit>
    {
        public int Id { get; init; }

        public class Handler : IRequestHandler<DeleteEntrancePassCommand, Unit>
        {
            EntrancePassService passService { get; init; }
            public Handler(EntrancePassService _passService)
            {
                passService = _passService;
            }
            public async Task<Unit> Handle(DeleteEntrancePassCommand request, CancellationToken cancellationToken)
            {
                await new DeleteEntrancePassValidator().ValidateAndThrowAsync(request, cancellationToken);

                await passService.DeleteFromDataBase(request.Id);

                return Unit.Value;
            }
        }
    }
}
