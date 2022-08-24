using MediatR;
using Persistence.Services.Database;

namespace Application.Ban.Commands
{
    public class CreateBanCommand : IRequest<Unit>
    {
        public string Reason { get; init; }
        public DateTime End { get; init; }

        public class Handler : IRequestHandler<CreateBanCommand, Unit>
        {
            private readonly BanService banService;
            public Handler(BanService _banService)
            {
                banService = _banService;
            }
        }
    }
}
