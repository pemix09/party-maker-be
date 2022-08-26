using Microsoft.AspNetCore.Identity;

namespace Persistence.Exceptions
{
    public class UserCannotBeCreatedException : BaseAppException
    {
        public UserCannotBeCreatedException(IEnumerable<IdentityError> _errors, int _code = 500) : base()
        {
            foreach (var error in _errors)
            {
                message += error.Description;
            }
            code = _code;
        }
    }
}
