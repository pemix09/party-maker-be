using MediatR;
using Persistence.Services.Database;

namespace Application.Ban.Commands
{
    using Application.Ban.Validators;
    using Core.Models;
    using FluentValidation;

    public class DeleteBanCommand : IRequest<Unit>
    {
        public int BanId { get; init; }

        public class Handler : IRequestHandler<DeleteBanCommand, Unit>
        {
            private readonly BanService banService;
            public Handler(BanService _banService)
            {
                banService = _banService;
            }

            public async Task<Unit> Handle(DeleteBanCommand _request, CancellationToken _cancellationToken)
            {
                await new DeleteBanValidator().ValidateAndThrowAsync(_request, _cancellationToken);

                await banService.DeleteFromDataBase(_request.BanId);

                return Unit.Value;
            }
        }
    }
}
