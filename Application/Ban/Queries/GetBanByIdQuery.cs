namespace Application.Ban.Queries
{
    using Core.Models;
    using MediatR;
    using Persistence.Services.Database;

    public class GetBanByIdQuery : IRequest<Ban>
    {
        public int BanID { get; init; }
        public class Handler : IRequestHandler<GetBanByIdQuery, Ban>
        {
            private readonly BanService banService;
            public Handler(BanService _banService)
            {
                banService = _banService;
            }

            public async Task<Ban> Handle(GetBanByIdQuery _request, CancellationToken _cancellationToken)
            {
                return await banService.GetByIdFromDataBase(_request.BanID);
            }
        }
    }
}
