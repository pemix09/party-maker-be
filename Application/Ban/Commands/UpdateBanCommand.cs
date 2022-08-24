using MediatR;
using Persistence.Services.Database;

namespace Application.Ban.Commands
{
    using Application.Ban.Validators;
    using Core.Models;
    using FluentValidation;

    public class UpdateBanCommand : IRequest<Unit>
    {
        public DateTime NewEndDate { get; init; }
        public int BanId { get; init; }
        
        public class Handler : IRequestHandler<UpdateBanCommand, Unit>
        {
            private readonly BanService banService;
            public Handler(BanService _banService)
            {
                banService = _banService;
            }
            public async Task<Unit> Handle(UpdateBanCommand _request, CancellationToken _cancellationToken)
            {
                await new UpdateBanValidator().ValidateAndThrowAsync(_request, _cancellationToken);

                Ban ban = await banService.GetByIdFromDataBase(_request.BanId);
                ban.SetNewEnd(_request.NewEndDate);
                await banService.UpdateInDataBase(ban);

                return Unit.Value;
            }
        }
    }
}
