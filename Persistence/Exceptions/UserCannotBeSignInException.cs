using Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Persistence.Exceptions
{
    public class UserCannotBeSignInException : BaseAppException
    {
        private int errorCode = 401;
        public UserCannotBeSignInException(string _userName) : base()
        {
            message = $"{_userName} cannot be signed in!";
            code = errorCode;
        }

        public UserCannotBeSignInException() : base()
        {
            message = "User couldn't have been signed in!";
            code = errorCode;
        }

        public UserCannotBeSignInException(SignInResult _result, AppUser _user) : base()
        {
            message = String.Join(
                        $"User: {_user.UserName} ",
                        $"RequireTwoFactor: {_result.RequiresTwoFactor} ",
                        $"IsNotAllowed: {_result.IsNotAllowed} ",
                        $"IsLockedOut: {_result.IsLockedOut} ");
            code = errorCode;
        }
    }
}
