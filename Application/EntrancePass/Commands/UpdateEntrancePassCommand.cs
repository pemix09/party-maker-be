using MediatR;
using Persistence.Services.Database;

namespace Application.EntrancePass.Commands
{
    using Application.EntrancePass.Validators;
    using Core.Models;
    using FluentValidation;

    public class UpdateEntrancePassCommand : IRequest<Unit>
    {
        public decimal Price { get; init; }
        public int Id { get; init; }

        public class Handler : IRequestHandler<UpdateEntrancePassCommand, Unit>
        {
            EntrancePassService passService { get; init; }
            public Handler(EntrancePassService _passService)
            {
                passService = _passService;
            }

            public async Task<Unit> Handle(UpdateEntrancePassCommand request, CancellationToken cancellationToken)
            {
                await new UpdateEntrancePassValidator().ValidateAndThrowAsync(request, cancellationToken);

                EntrancePass toUpdate = await passService.GetByIdFromDataBase(request.Id);
                toUpdate.SetPrice(request.Price);
                await passService.UpdateInDataBase(toUpdate);

                return Unit.Value;
            }
        }
    }
}
