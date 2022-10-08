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
            private readonly UserService userService;
            public Handler(BanService _banService, UserService _userService)
            {
                banService = _banService;
                userService = _userService;
            }
            public async Task<Unit> Handle(CreateBanCommand _request, CancellationToken _cancellationToken)
            {
                await new CreateBanValidator().ValidateAndThrowAsync(_request, _cancellationToken);

                AppUser responsibleAdmin = await userService.GetCurrentlySignedIn();
                
                //check if user with given Id exists
                AppUser bannedUser = await userService.GetUserById(_request.BannedUser);
                
                Ban ban = Ban.Create(_request.Reason, _request.End, bannedUser.Id, responsibleAdmin.Id);
                await banService.AddToDataBase(ban);

                return Unit.Value;
            }
        }
    }
}
