using MediatR;
using Persistence.Services.Database;

namespace Application.Ban.Queries
{
    using Core.Models;
    public class GetAllBansQuery : IRequest<IEnumerable<Ban>>
    {
        public class Handler : IRequestHandler<GetAllBansQuery, IEnumerable<Ban>>
        {
            private readonly BanService banService;
            public Handler(BanService _banService)
            {
                banService = _banService;
            }
            public async Task<IEnumerable<Ban>> Handle(GetAllBansQuery _request, CancellationToken _cancellationToken)
            {
                return await banService.GetAllFromDataBase();
            }
        }
    }
}
