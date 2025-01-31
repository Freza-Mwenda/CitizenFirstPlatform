using System.Text.Json.Serialization;

namespace CitizenFirstUssd.Services;

public class KycResponse
{
    [JsonPropertyName("StatusCode")] public int StatusCode { get; set; }
    [JsonPropertyName("first_name")] public string FirstName { get; set; }
    [JsonPropertyName("last_name")] public string LastName { get; set; }
}