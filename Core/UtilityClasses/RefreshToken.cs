namespace Core.UtilityClasses;

public class RefreshToken
{ 
        //Default values
        public string Token { get; set; } = string.Empty;
        public DateTime Created { get; init; } = DateTime.Now;
        public DateTime Expires { get; init; } 
}