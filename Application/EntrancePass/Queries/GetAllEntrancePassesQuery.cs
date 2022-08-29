using MediatR;

namespace Application.EntrancePass.Queries
{
    using Core.Models;
    using Persistence.Services.Database;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetAllEntrancePassesQuery : IRequest<IEnumerable<EntrancePass>>
    {
        public class Handler : IRequestHandler<GetAllEntrancePassesQuery, IEnumerable<EntrancePass>>
        {
            EntrancePassService passService { get; init; }
            public Handler(EntrancePassService _passService)
            {
                passService = _passService;
            }
            public async Task<IEnumerable<EntrancePass>> Handle(GetAllEntrancePassesQuery request, CancellationToken cancellationToken)
            {
                return await passService.GetAllFromDataBase();
            }
        }
    }
}
