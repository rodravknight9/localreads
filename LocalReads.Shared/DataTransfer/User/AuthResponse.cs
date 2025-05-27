using System.Text.Json.Serialization;

namespace LocalReads.Shared.DataTransfer.User;

public class AuthResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("userName")]
    public string UserName { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("jwt")]
    public string Jwt { get; set; }
}