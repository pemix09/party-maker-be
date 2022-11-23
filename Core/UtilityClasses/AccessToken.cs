using System.Text.Json;

namespace Core.UtilityClasses
{
    public class AccessToken
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Created { get; init; } = DateTime.Now;
        public DateTime Expires { get; init; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
