using System.Text.Json;

namespace Core.UtilityClasses
{
    public class AccessToken
    {
        public string Token { get; set; } = string.Empty;
        public DateTimeOffset Created { get; init; } = DateTimeOffset.Now;
        public DateTimeOffset Expires { get; init; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
