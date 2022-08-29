using MediatR;

namespace Application.EntrancePass.Queries
{
    using Application.EntrancePass.Validators;
    using Core.Models;
    using FluentValidation;
    using Persistence.Services.Database;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetEntrancePassByIdQuery : IRequest<EntrancePass>
    {
        public int Id { get; init; }

        public class Handler : IRequestHandler<GetEntrancePassByIdQuery, EntrancePass>
        {
            EntrancePassService passService { get; init; }
            public Handler(EntrancePassService _passService)
            {
                passService = _passService;
            }
            public async Task<EntrancePass> Handle(GetEntrancePassByIdQuery request, CancellationToken cancellationToken)
            {
                await new GetEntrancePassByIdValidator().ValidateAndThrowAsync(request, cancellationToken);

                return await passService.GetByIdFromDataBase(request.Id);
            }
        }
    }
}
