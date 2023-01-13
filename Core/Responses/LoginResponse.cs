namespace Core.UtilityClasses
{
    public class LoginResponse
    {
        public LoginResponse(AccessToken accessToken, RefreshToken refreshToken) 
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
        public AccessToken AccessToken { get; init; }
        public RefreshToken RefreshToken { get; init; }
    }
}
