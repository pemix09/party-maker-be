using MediatR;
using Persistence.Services.Database;

namespace Application.EntrancePass.Commands
{
    using Application.EntrancePass.Validators;
    using Core.Models;
    using FluentValidation;

    public class CreateEntrancePassCommand : IRequest<Unit>
    {
        public string PassType { get; init; }

        public class Handler : IRequestHandler<CreateEntrancePassCommand, Unit>
        {
            EntrancePassService passService { get; init; }
            public Handler(EntrancePassService _passService)
            {
                passService = _passService;
            }
            public async Task<Unit> Handle(CreateEntrancePassCommand request, CancellationToken cancellationToken)
            {
                await new CreateEntrancePassValidator().ValidateAndThrowAsync(request,cancellationToken);

                EntrancePass pass = EntrancePass.Create(request.PassType);
                await passService.AddToDataBase(pass);

                return Unit.Value;
            }
        }
    }
}
