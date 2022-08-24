using Application.Ban.Validators;
using FluentValidation;
using MediatR;
using Persistence.Services.Database;

namespace Application.Ban.Commands
{
    using Core.Models;
    public class CreateBanCommand : IRequest<Unit>
    {
        public string Reason { get; init; }
        public DateTime End { get; init; }
        public string BannedUser { get; init; }

        public class Handler : IRequestHandler<CreateBanCommand, Unit>
        {
            private readonly BanService banService;
            public Handler(BanService _banService)
            {
                banService = _banService;
            }
            public async Task<Unit> Handle(CreateBanCommand _request, CancellationToken _cancellationToken)
            {
                await new CreateBanValidator().ValidateAndThrowAsync(_request, _cancellationToken);

                //TODO - when user service is ready
                Ban ban = Ban.Create(_request.Reason, _request.End, _request.BannedUser, _request.BannedUser);
                await banService.AddToDataBase(ban);

                return Unit.Value;
            }
        }
    }
}
