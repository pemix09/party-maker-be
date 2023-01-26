using Microsoft.AspNetCore.Identity;

namespace Persistence.Exceptions
{
    public class UserCannotBeCreatedException : BaseAppException
    {
        private const int exCode = StatusCodes.Status400BadRequest;
        public UserCannotBeCreatedException(IEnumerable<IdentityError> _errors) : base()
        {
            foreach (var error in _errors)
            {
                message += error.Description;
            }

            base.code = exCode;
        }
    }
}
