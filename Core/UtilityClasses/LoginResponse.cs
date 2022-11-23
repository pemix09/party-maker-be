namespace Core.UtilityClasses
{
    public class LoginResponse
    {
        public LoginResponse(string accessToken, string refreshToken) 
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
        public string AccessToken { get; init; }
        public string RefreshToken { get; init; }
    }
}
