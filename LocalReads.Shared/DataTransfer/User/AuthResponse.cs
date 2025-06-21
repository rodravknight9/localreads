using System.Text.Json.Serialization;

namespace LocalReads.Shared.DataTransfer.User;

public class AuthResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("userName")]
    public string UserName { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("jwt")]
    public string Jwt { get; set; } = string.Empty;
}