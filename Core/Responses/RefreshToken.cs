namespace Core.UtilityClasses;
using System.Text.Json;
using System.Text.Json.Serialization;
public class RefreshToken
{ 
        //Default values
        public string Token { get; set; } = string.Empty;
        public DateTimeOffset Created { get; init; } = DateTimeOffset.Now;
        public DateTimeOffset Expires { get; init; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}